import React, { useEffect, useRef, useState } from 'react';
import './App.css';
import SideBar from './components/Sidebar';
import { Grid } from '@mui/material';
import ImageFrame from './components/ImageFrame';
import BoxDTO from './models/BoxDTO';
import { RECT_COLORS } from './Constants';

const firstColorEntry = RECT_COLORS.entries().next().value;

function App() 
{
  const [imageSrc, setImageSrc] = useState<string>("placeholder.png");
  const [boxes, setBoxes] = useState<BoxDTO[]>([]);
  const [rectColor, setRectColor] = useState(firstColorEntry[1]);
  const [confidenceLevel, setConfidenceLevel] = useState(80);

  function updateImage(newImageSrc: string)
  {
    setImageSrc(newImageSrc);
  }

  function updateBoxes(newBoxes: BoxDTO[])
  {
    setBoxes(newBoxes);
  }

  function updateRectColor(newRectColor: string)
  {
    setRectColor(newRectColor);
  }

  function updateConfidenceLevel(newConfidenceLevel: number)
  {
    setConfidenceLevel(newConfidenceLevel);
  }

  return (
    <div className="App">
      <header className="App-header">
        <Grid container spacing={2}>
          <Grid item xs={3}>
            <SideBar 
              updateBoxes={updateBoxes}
              updateImage={updateImage}
              updateRectColor={updateRectColor}
              updateConfidenceLevel={updateConfidenceLevel}
              defaultRectColor={firstColorEntry[0]}
              defaultConfidenceLevel={confidenceLevel}
            />
          </Grid>
          <Grid item xs={9}>
            <ImageFrame
              imageSrc={imageSrc}
              boxes={boxes}
              rectColor={rectColor}
              confidenceLevel={confidenceLevel}
            />
          </Grid>
        </Grid>
      </header>
    </div>
  );
}

export default App;
