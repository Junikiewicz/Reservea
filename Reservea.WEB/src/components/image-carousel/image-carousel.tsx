import React from "react";
import { Carousel} from "react-bootstrap";

const ImageCarousel = () => (
    <Carousel>
      <Carousel.Item>
        <img
          className="d-block w-100"
          src="img/zdjecie1.jpg"
          alt="First slide"
          style={{height: "400px" }}
        />
        <Carousel.Caption>
          <h3>First slide label</h3>
          <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img
          className="d-block w-100"
          src="img/zdjecie2.jpg"
          alt="Third slide"
          style={{height: "400px" }}
        />
        <Carousel.Caption>
          <h3>Second slide label</h3>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img
          className="d-block w-100"
          src="img/zdjecie3.jpg"
          alt="Third slide"
          style={{height: "400px" }}
        />
        <Carousel.Caption>
          <h3>Third slide label</h3>
          <p>
            Praesent commodo cursus magna, vel scelerisque nisl consectetur.
          </p>
        </Carousel.Caption>
      </Carousel.Item>
    </Carousel>
);

export default ImageCarousel;
