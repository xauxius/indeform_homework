import { Button, Divider, MenuItem, Select, SelectChangeEvent, Slider } from '@mui/material';
import { styled } from '@mui/material/styles';
import Stack from '@mui/material/Stack';
import { useEffect, useState } from 'react';
import objectDetectionClient from '../clients/ObjectDetectionClient';
import ModelInfoDTO from '../models/ModelInfoDTO';
import { RECT_COLORS } from '../Constants';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import BoxDTO from '../models/BoxDTO';

const VisuallyHiddenInput = styled('input')({
  clip: 'rect(0 0 0 0)',
  clipPath: 'inset(50%)',
  height: 1,
  overflow: 'hidden',
  position: 'absolute',
  bottom: 0,
  left: 0,
  whiteSpace: 'nowrap',
  width: 1,
});

interface SideBarProps {
    updateImage: (image: string) => void;
    updateBoxes: (boxes: BoxDTO[]) => void;
    updateRectColor: (rectColor: string) => void;
    updateConfidenceLevel: (confidenceLevel: number) => void;
    defaultRectColor: string;
    defaultConfidenceLevel: number;
}

function SideBar(props: SideBarProps)
{
    const [models, setModels] = useState<ModelInfoDTO[]>([]);
    const [selectedModel, setSelectedModel] = useState<number>();
    const [selectedImageFile, setSelectedImageFile] = useState<File | null>(null)

    useEffect(() => {
        objectDetectionClient.fetchAllModels().then((res) => setModels(res));
    }, [])

    function updateSelectedModel(event: SelectChangeEvent<number>)
    {
        setSelectedModel(event.target.value as number);
    }

    function updateConfidenceLevel(event: Event, newValue: number | number[])
    {
        props.updateConfidenceLevel(newValue as number);
    }

    function updateRectColor(event: SelectChangeEvent<string>)
    {
        const key = event.target.value;
        const rectColor = RECT_COLORS.get(key);
        rectColor && props.updateRectColor(rectColor);
    }

    function handleImageChange(event: React.ChangeEvent<HTMLInputElement>)
    {
        const file = event.target.files?.[0];

        if (file)
        {
            const srcImage = URL.createObjectURL(file);
            setSelectedImageFile(file)
            props.updateImage(srcImage);
        }
    }

    function predict()
    {
        if (selectedModel && selectedImageFile)
        {
            objectDetectionClient.getPrediction(selectedModel, selectedImageFile).then(res => {
                props.updateBoxes(res)
            });
        }
    }

    return <Stack spacing={2} paddingTop={10} paddingLeft={2}>
        <p>Pasirinkite modelį:</p>
        <Select 
            value={selectedModel}
            defaultValue={models.length > 0 ? models[0].id : NaN}
            onChange={updateSelectedModel}
        >
            {
                models.map((modelInfo) => 
                    <MenuItem value={modelInfo.id}>
                        {modelInfo.name}
                    </MenuItem>
                )
            }
        </Select>
        <Button 
            component="label"
            role={undefined}
            tabIndex={-1}
            variant="contained"
            startIcon={<CloudUploadIcon />}
        >
            Įkelti paveiksliuką
            <VisuallyHiddenInput type="file" accept="image/*" onChange={handleImageChange}/>
        </Button>
        <Button variant="contained" onClick={predict} disabled={!(selectedImageFile && selectedModel)}>
            Rasti objektus
        </Button>
        <Divider/>
        <p>Užtikrintumo lygis:</p>
        <Slider 
            defaultValue={props.defaultConfidenceLevel}
            // value={confidenceLevel}
            onChange={updateConfidenceLevel}
            valueLabelDisplay="auto" 
        />
        <p>Stačiakampių spalva</p>
        <Select
            defaultValue={props.defaultRectColor}
            onChange={updateRectColor}
        >
            {
                Array.from(RECT_COLORS).map(([key, _]) =>
                <MenuItem key={key} value={key}>
                    {key}
                </MenuItem>
                )
            }
        </Select>
    </Stack>
}

export default SideBar