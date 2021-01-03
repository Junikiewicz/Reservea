import React, { useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import {
  deleteImage,
  getAllImages,
  getAllTextFieldsContentsRequest,
  Photo,
  TextFieldContent,
  updateTextFieldContentsRequest,
  uploadImage,
} from "../../../api/clients/cmsClients";
import ImageUploader from "react-images-upload";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash } from "@fortawesome/free-solid-svg-icons";
import "./home-page-managment.css";

function HomePageManagment() {
  const { register, handleSubmit, reset, getValues, formState } = useForm<any>({
    mode: "onSubmit",
  });
  const [textFieldsContents, setTextFieldsContents] = useState<
    Array<TextFieldContent>
  >([]);
  const [ImageUploaderKey, setImageUploaderKey] = useState(0);

  const [uploadedPictures, setUploadedPictures] = useState<Array<any>>([]);
  const [pictures, setPictures] = useState<Array<any>>([]);
  const imageUploader = React.useRef<any>(null);

  useEffect(() => {
    getAllImages()
      .then((response: Array<Photo>) => {
        setPictures(response);
      })
      .catch(() => {});
  }, []);

  const onDrop = (picture: any) => {
    setUploadedPictures([picture]);
  };

  const uploadImages = async () => {
    let createdPictures: Array<Photo> = [];

    for (const picture of uploadedPictures[0]) {
      await uploadImage(picture).then((createdPicture: Photo) => {
        createdPictures.push(createdPicture);
      });
    }
    setPictures(pictures.concat(createdPictures));
    toast.success("Zdjęcia zostały dodane");
    setImageUploaderKey(ImageUploaderKey + 1);
    setUploadedPictures([]);
  };

  useEffect(() => {
    getAllTextFieldsContentsRequest()
      .then((response: Array<TextFieldContent>) => {
        setTextFieldsContents(response);
      })
      .catch(() => {});
  }, []);

  const deletePicture = async (id: number) => {
    deleteImage(id).then(() => {
      toast.success("Usunięto zdjęcie");
      let newPictures = [...pictures];
      newPictures = newPictures.filter((x) => x.id !== id);
      setPictures(newPictures);
    });
  };

  const onSubmit = async (data: any): Promise<void> => {
    let request = [...textFieldsContents];
    request.find((x) => x.name === "aboutUs")!.content = data.aboutUs;
    request.find((x) => x.name === "contact")!.content = data.contact;

    updateTextFieldContentsRequest(request)
      .then(() => {
        reset(data);
        setTextFieldsContents(request);
        toast.success("Zmiany zostały zapisane");
      })
      .catch(() => {});
  };
  return (
    <div className="pageContent mt-4">
      <Container>
        <Form onSubmit={handleSubmit(onSubmit)}>
          <Row className="mt-3">
            <Col>
              <Form.Group>
                <h3>O nas</h3>
                <Form.Control
                  id="aboutUs"
                  name="aboutUs"
                  className="bg-dark text-light"
                  as="textarea"
                  ref={register()}
                  defaultValue={
                    textFieldsContents.find((x) => x.name === "aboutUs")
                      ?.content
                  }
                  rows={6}
                />
              </Form.Group>
              <Form.Group>
                <h3>Kontakt</h3>
                <Form.Control
                  id="contact"
                  name="contact"
                  className="bg-dark text-light"
                  as="textarea"
                  defaultValue={
                    textFieldsContents.find((x) => x.name === "contact")
                      ?.content
                  }
                  ref={register()}
                  rows={6}
                />
              </Form.Group>
              <Row>
                <Button
                  variant="success"
                  type="submit"
                  disabled={!formState.isDirty}
                  className="ml-auto mr-3 mb-3"
                >
                  Zapisz zmiany
                </Button>
              </Row>
            </Col>
            <Col>
              <h3>Zdjęcia</h3>
              <div className="container">
                <div className="row text-center">
                  {pictures.map((element) => (
                    <div className="card mt-2 col-lg-3 col-md-5 col-sm-8 col-8">
                      <img src={element.url} className="card-img-top" />
                      <div className="text-center delete-button animate">
                        <button
                          onClick={() => {
                            deletePicture(element.id);
                          }}
                          type="button"
                          className="btn btn-danger"
                        >
                          <FontAwesomeIcon size="lg" icon={faTrash} />
                        </button>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
              <Row className="justify-content-center">
                <Col className="col-10">
                  <ImageUploader
                    style={{ backgroudColor: "black" }}
                    withIcon={true}
                    onChange={onDrop}
                    key={ImageUploaderKey}
                    buttonText="Wybierz zdjęcie"
                    label="Maksymalna wielkość pliku: 5mb, formaty: jpg/gif/png"
                    withPreview={true}
                    imgExtension={[".jpg", ".gif", ".png", ".gif"]}
                    maxFileSize={5242880}
                    ref={imageUploader}
                  />
                  <Row>
                    <Button
                      onClick={uploadImages}
                      variant="success"
                      type="submit"
                      disabled={!(uploadedPictures.length > 0)}
                      className="ml-auto mr-3 mb-3"
                    >
                      Dodaj zdjęcia
                    </Button>
                  </Row>
                </Col>
              </Row>
            </Col>
          </Row>
        </Form>
      </Container>
    </div>
  );
}

export default HomePageManagment;
