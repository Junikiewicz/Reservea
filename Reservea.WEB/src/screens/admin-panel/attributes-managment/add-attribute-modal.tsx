import React, { useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";

function AddAttributeModal({ handleClose, handleChoose, show }: any) {
  const [selectedElement, setSelectedElement] = useState<string>();

  return (
    <Modal className="my-modal" show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Wpisz nazwę nowego atrybutu</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form.Control
          id="resourceTypeId"
          type="text"
          onChange={(e) => {
            setSelectedElement(e.target.value);
          }}
          value={selectedElement}
          name="resourceTypeId"
          className="bg-dark text-light"
        />
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Anuluj
        </Button>
        <Button
          variant="success"
          disabled={selectedElement?.length === 0}
          onClick={() => {
            handleChoose(selectedElement);
          }}
        >
          Dodaj
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default AddAttributeModal;
