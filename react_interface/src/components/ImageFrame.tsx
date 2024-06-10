import { useEffect, useRef, useState } from "react";
import BoxDTO from "../models/BoxDTO";

interface ImageProps
{
    imageSrc: string;
    boxes: BoxDTO[];
    rectColor: string;
    confidenceLevel: number;
}

function ImageFrame(props: ImageProps)
{
    const canvasRef = useRef<HTMLCanvasElement | null>(null);
    const [imageWidth, setImageWidth] = useState(0);
    const [imageHeight, setImageHeight] = useState(0);

    useEffect(() => {
        const canvas = canvasRef.current;
        if (canvas) 
        {
            const context = canvas.getContext("2d");
            if (context) 
            {
                const width = context.canvas.width;
                const height = context.canvas.height;

                const centerX = width / 2;
                const centerY = height / 2;

                context.clearRect(0, 0, width, height);

                const image = new Image();
                image.src = props.imageSrc;
                image.onload = () => {
                    setImageWidth(image.width);
                    setImageHeight(image.height)
                    context.drawImage(image, centerX - image.width / 2, centerY - image.height / 2, image.width, image.height);
                
                    props.boxes.forEach((box) => {
                        if (box.score >= props.confidenceLevel)
                        {
                            console.log(props.rectColor);
                            context.lineWidth = 3;
                            context.strokeStyle = props.rectColor;
                            context.strokeRect(box.x1, box.y1, box.x2 - box.x1, box.y2 - box.y1);
                        }
                    });
                };
            }
        }

    }, [props.imageSrc, props.boxes, props.rectColor, props.confidenceLevel, imageHeight, imageWidth]);

    return <center>
        <canvas 
            ref = {canvasRef}
            width={imageWidth}
            height={imageHeight}
        />
    </center>
}

export default ImageFrame;