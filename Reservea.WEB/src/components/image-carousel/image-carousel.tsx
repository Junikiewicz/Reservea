import React from "react";
import { Carousel} from "react-bootstrap";

const ImageCarousel = () => (
    <Carousel className="my-carousel">
      <Carousel.Item>
        <img
          className="d-block w-100"
          src="img/zdjecie1.jpg"
          alt="First slide"
          style={{height: "400px" }}
        />
      </Carousel.Item>
      <Carousel.Item>
        <img
          className="d-block w-100"
          src="img/zdjecie2.jpg"
          alt="Third slide"
          style={{height: "400px" }}
        />
      </Carousel.Item>
      <Carousel.Item>
        <img
          className="d-block w-100"
          src="img/zdjecie3.jpg"
          alt="Third slide"
          style={{height: "400px" }}
        />
      </Carousel.Item>
    </Carousel>
);

export default ImageCarousel;
