import axios, { AxiosInstance } from "axios";
import { OBJECT_DETECTION_SERVER } from "../Constants";
import { DefaultDeserializer } from "v8";
import ModelInfoDTO from "../models/ModelInfoDTO";
import BoxDTO from "../models/BoxDTO";

class ObjectDetectionClient
{
    private readonly baseClient: AxiosInstance;

    constructor(server_address: string)
    {
        this.baseClient = axios.create({
            "baseURL": server_address,
            "headers": {
                "Content-Type": "application/json"
            }
        });
    }

    async fetchAllModels() : Promise<ModelInfoDTO[]>
    {
        const response = await this.baseClient.get("/models");
        console.log(response.data)

        return response.data
    }

    async getPrediction(model_id: number, image: File) : Promise<BoxDTO[]>
    {
        const formData = new FormData();
        formData.append("image", image)
        const headers = {"Content-Type": "multipart/form-data"}

        const response = await this.baseClient.post<BoxDTO[]>(
            "/predict/" + model_id.toString(), formData, {headers: headers}
        );

        return response.data;
    }
}

const objectDetectionClient = new ObjectDetectionClient(OBJECT_DETECTION_SERVER);

export default objectDetectionClient;