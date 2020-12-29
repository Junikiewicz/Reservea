import {
  faLongArrowAltLeft,
  faTrashAlt,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { ReactText, useEffect, useState } from "react";
import { Button, Col, Container, Row, Table } from "react-bootstrap";
import Timeline, {
  ReactCalendarItemRendererProps,
} from "react-calendar-timeline";
import "react-calendar-timeline/lib/Timeline.css";
import { Link, useHistory } from "react-router-dom";
import { toast } from "react-toastify";
import {
  createReservation,
  getResourceTypeReservations,
  ReservationRequest,
} from "../../api/clients/reservationsClient";
import { resourceTypeAvaliabilitiesRequest } from "../../api/clients/resourcesClient";
import { ResoucerTypeAvaliabilitiesResponse } from "../../api/dtos/resources/resources/resoucerTypeAvaliabilitiesResponse";
import { checkIfLoggedIn } from "../../common/helpers/jwtTokenHelper";
import "./resource-type-timeline.css";

function addHours(date: Date, hours: number) {
  var result = new Date(date);
  result.setTime(result.getTime() + hours * 60 * 60 * 1000);
  return result;
}

function addMinutes(date: Date, minutes: number) {
  var result = new Date(date);
  result.setTime(result.getTime() + minutes * 60 * 1000);
  return result;
}
function timeout(delay: number) {
  return new Promise((res) => setTimeout(res, delay));
}

export interface TimelineItems {
  items: Array<any>;
  groups: Array<any>;
  selected: Array<number>;
}

function ResourceTypeTimeline(props: any) {
  const [items, setItems] = useState<Array<any>>([]);
  const [selected, setSelected] = useState<Array<number>>([]);
  const [groups, setGroups] = useState<Array<any>>([]);
  const [isSelectingPlace, setIsSelectingPlace] = useState<boolean>(false);
  const [itemsIterator, setItemsIterator] = useState<number>(0);
  const history = useHistory();

  useEffect(() => {
    const resourceType = props.match.params.resourceTypeID;
    let itemsIterator = 1;
    const min = new Date();
    const max = addHours(new Date(), 24 * 7 * 4);

    resourceTypeAvaliabilitiesRequest(resourceType) //TODO: Remove chain
      .then(async (response: Array<ResoucerTypeAvaliabilitiesResponse>) => {
        getResourceTypeReservations(resourceType)
          .then(async (reservationsResponse: Array<ReservationRequest>) => {
            const timelineGroups = response.map((x) => ({
              id: x.id,
              title: x.name,
            }));
            let availabilities: any[] = [];
            const reservations: Array<ReservationRequest> = reservationsResponse.map(
              (x) => ({
                start: new Date(x.start),
                end: new Date(x.end),
                resourceId: x.resourceId,
              })
            );

            for (let resource of response) {
              let temp = resource.resourceAvailabilities.map((x) => {
                if (x.interval) {
                  const a = x.interval.replace(/\./g, ":").split(":"); //TEMP
                  if (a.length > 3) {
                    x.interval = +a[0] * 60 * 24 + +a[1] * 60 + +a[2];
                  } else {
                    x.interval = +a[0] * 60 + +a[1];
                  }

                  return {
                    resourceId: x.resourceId,
                    isReccuring: x.isReccuring,
                    interval: x.interval,
                    start: new Date(x.start),
                    end: new Date(x.end),
                  };
                }

                return {
                  resourceId: x.resourceId,
                  isReccuring: x.isReccuring,
                  interval: null,
                  start: new Date(x.start),
                  end: new Date(x.end),
                };
              });
              availabilities = availabilities.concat(temp);
            }

            let timelineItems: Array<any> = [];

            //Not reccuring
            for (const availability of availabilities.filter(
              (x) => x.isReccuring === false
            )) {
              timelineItems.push({
                id: itemsIterator,
                group: availability.resourceId,
                canMove: false,
                canResize: false,
                start_time: availability.start.getTime(),
                end_time: availability.end.getTime(),
                isReservation: false,
                newReservation: false,
              });
              itemsIterator += 1;
            }
            //Recurring
            for (const availability of availabilities.filter(
              (x) => x.isReccuring === true
            )) {
              let temp = { ...availability };
              temp.start = new Date(temp.start);
              temp.end = new Date(temp.end);
              while (temp.start < max) {
                timelineItems.push({
                  id: itemsIterator,
                  group: temp.resourceId,
                  canMove: false,
                  canResize: false,
                  start_time: temp.start.getTime(),
                  end_time: temp.end.getTime(),
                  isReservation: false,
                  newReservation: false,
                });
                itemsIterator += 1;
                temp.start = addMinutes(temp.start, temp.interval);
                temp.end = addMinutes(temp.end, temp.interval);
              }
            }
            for (const reservation of reservations) {
              timelineItems.push({
                id: itemsIterator,
                canMove: false,
                canResize: false,
                group: reservation.resourceId,
                start_time: reservation.start.getTime(),
                end_time: reservation.end.getTime(),
                isReservation: true,
                newReservation: false,
              });
              itemsIterator += 1;
            }

            setGroups(timelineGroups);
            setItems(timelineItems);
            setItemsIterator(itemsIterator);
          })
          .catch(() => {});
      })
      .catch(() => {});
  }, []);

  const startSelectingPlace = () => {
    setIsSelectingPlace(true);
  };

  const itemRenderer = (props: ReactCalendarItemRendererProps<any>) => {
    const background = props.item.newReservation
      ? "blue"
      : props.item.isReservation
      ? "red"
      : "grey";
    const border = background;
    return (
      <div
        {...props.getItemProps({
          style: {
            background,
            border,
          },
        })}
      ></div>
    );
  };
  let draggedItem: any = {};
  let isDragging = false;

  const onItemDrag = (itemDragObject: any) => {
    if (!isDragging) {
      const item = items.find((x) => x.id === itemDragObject.itemId);
      draggedItem.orginal_start_time = item.start_time;
      draggedItem.orginal_end_time = item.end_time;
      draggedItem.orginal_group = item.group;
      isDragging = true;
    }

    draggedItem.time = itemDragObject.time;
    draggedItem.eventType = itemDragObject.eventType;
    draggedItem.itemId = itemDragObject.itemId;
    draggedItem.edge = itemDragObject.edge;
    draggedItem.newGroupOrder = itemDragObject.newGroupOrder;
  };

  const onItemSelect = (itemId: ReactText, e: any, time: number) => {
    onCanvasClick(items.find((x) => x.id === Number(itemId)).group, time, e);
  };

  const onItemClick = (itemId: ReactText, e: any, time: number) => {
    onCanvasClick(items.find((x) => x.id === Number(itemId)).group, time, e);
  };

  const validate = (newItems: Array<any>, movedItemId: number): boolean => {
    let newItem = newItems.find((x) => x.id === movedItemId);
    let otherReservations = newItems.filter(
      (x) =>
        (x.isReservation === true || x.newReservation === true) &&
        x.id !== newItem.id &&
        x.group === newItem.group
    );
    let availabilities = newItems.filter(
      (x) => x.isReservation === false && x.group === newItem.group
    );

    //check if colide with any item
    for (let reservation of otherReservations) {
      if (
        reservation.start_time < newItem.end_time &&
        reservation.end_time > newItem.start_time
      ) {
        return false;
      }
    }

    //check if inside avaiability
    let iterator = newItem.start_time;

    while (iterator < newItem.end_time) {
      let filtered = availabilities.filter(
        (x) => x.start_time <= iterator && x.end_time > iterator
      );
      if (filtered.length === 0) {
        return false;
      }

      let largestAvailability = filtered.reduce((prev, current) =>
        prev.end_time > current.end_time ? prev : current
      );
      if (largestAvailability) {
        iterator = largestAvailability.end_time;
      } else {
        return false;
      }
    }

    return true;
  };

  const onMouseUpTemp = () => {
    if (isDragging) {
      let newItems = [...items];
      let itemIndex = newItems.findIndex((x) => x.id === draggedItem.itemId);
      if (draggedItem.eventType === "move") {
        const diffrence = newItems[itemIndex].start_time - draggedItem.time;
        newItems[itemIndex].start_time = draggedItem.time;
        newItems[itemIndex].end_time = newItems[itemIndex].end_time - diffrence;
        newItems[itemIndex].group = groups[draggedItem.newGroupOrder].id;
      } else {
        newItems[itemIndex].end_time = draggedItem.time;
      }
      if (validate(newItems, draggedItem.itemId)) {
        setItems(newItems);
      } else {
        newItems[itemIndex].start_time = draggedItem.orginal_start_time;
        newItems[itemIndex].end_time = draggedItem.orginal_end_time;
        newItems[itemIndex].group = draggedItem.orginal_group
          ? draggedItem.orginal_group
          : newItems[itemIndex].group;
      }

      isDragging = false;
    }
  };

  const sendNewReservations = () => {
    const reservations = items
      .filter((x) => x.newReservation)
      .map((x) => ({
        start: new Date(x.start_time),
        end: new Date(x.end_time),
        resourceId: x.group,
      }));
    createReservation(reservations)
      .then(() => {
        toast.success("Pomyślnie utworzono rezerwacje");
        history.push("/user-reservations");
      })
      .catch(() => {});
  };

  const removeItem = async (itemId: number) => {
    let newItems = [...items];
    let newSelected = [...selected];
    newSelected = newSelected.filter((x) => x != itemId);
    newItems = newItems.filter((x) => x.id != itemId);
    setItems(newItems);
    await timeout(1); //xD
    setSelected(newSelected);
  };

  const onCanvasClick = async (groupId: ReactText, time: number, e: any) => {
    if (isSelectingPlace) {
      let newItems = [...items];
      const newItem = {
        id: itemsIterator,
        canMove: true,
        group: groupId,
        start_time: time,
        selected: true,
        end_time: time + 1000 * 60 * 60,
        newReservation: true,
      };

      newItems.push(newItem);
      let newSelected = [...selected];
      newSelected.push(newItem.id);
      if (validate(newItems, itemsIterator)) {
        setItemsIterator(itemsIterator + 1);
        setItems(newItems);
        await timeout(1); //xD
        setSelected(newSelected);
        setIsSelectingPlace(false);
      }
    }
  };

  return (
    <div>
      <div className="pageHeader">
        <Container>
          <Row>
            <Col className="col-4 mt-3">
              <Link className="customLink" to="/reservations">
                <FontAwesomeIcon
                  className="mr-2"
                  size="lg"
                  icon={faLongArrowAltLeft}
                ></FontAwesomeIcon>
                Lista typów zasobów
              </Link>
            </Col>
            <Col className="text-center mt-2 col-4">
              <h2>Samochody osobowe</h2>
            </Col>
          </Row>
        </Container>
      </div>

      <div className="pageContent mt-3">
        <Container className="mt-3">
          <Row>
            <Col className="">
              <h3>Lista terminów oraz rezerwacji:</h3>
            </Col>
            {!checkIfLoggedIn() && (
              <span className="col-5 mt-2" style={{ color: "red" }}>
                Przed złożeniem rezerwacji musisz się zalogować
              </span>
            )}
            <Col className="col-2">
              <Button
                disabled={!checkIfLoggedIn()}
                onClick={startSelectingPlace}
                variant={"success"}
              >
                Dodaj rezerwacje
              </Button>
            </Col>
          </Row>
          <div className="mt-3" onMouseUp={onMouseUpTemp}>
            {groups.length > 0 && items.length > 0 && (
              <Timeline
                groups={groups}
                items={items}
                onItemDrag={onItemDrag}
                selected={selected}
                itemHeightRatio={0.95}
                defaultTimeStart={addHours(new Date(), -12)}
                itemRenderer={itemRenderer}
                defaultTimeEnd={addHours(new Date(), 12)}
                onCanvasClick={onCanvasClick}
                onItemClick={onItemClick}
                onItemSelect={onItemSelect}
              />
            )}
          </div>
          {items.filter((x) => x.newReservation).length > 0 && (
            <div>
              <Row className="mt-5">
                <Col>
                  <h3>Wybrane rezerwacje:</h3>
                </Col>
              </Row>

              <Table
                striped
                bordered
                hover
                variant="dark"
                className="text-center"
              >
                <thead>
                  <tr>
                    <th>Zasób</th>
                    <th>Start</th>
                    <th>Koniec</th>
                    <th></th>
                  </tr>
                </thead>
                <tbody>
                  {items
                    .filter((x) => x.newReservation)
                    .map((element: any) => (
                      <tr key={element.id}>
                        <td>
                          {groups.find((x) => x.id == element.group).title}
                        </td>
                        <td>{new Date(element.start_time).toLocaleString()}</td>
                        <td>{new Date(element.end_time).toLocaleString()}</td>
                        <td>
                          <FontAwesomeIcon
                            size="lg"
                            icon={faTrashAlt}
                            onClick={() => removeItem(element.id)}
                            style={{ cursor: "pointer" }}
                          />
                        </td>
                      </tr>
                    ))}
                </tbody>
              </Table>
            </div>
          )}
        </Container>
      </div>
      {items.filter((x) => x.newReservation).length > 0 && (
        <Row className="mt-2">
          <Button
            className="ml-auto mr-3"
            onClick={sendNewReservations}
            variant={"success"}
          >
            Złóż rezerwacje
          </Button>
        </Row>
      )}
    </div>
  );
}

export default ResourceTypeTimeline;
