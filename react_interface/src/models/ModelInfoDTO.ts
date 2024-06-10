class ModelInfoDTO
{
    public name: String;
    public id: number; 

    constructor(name: String, ID: number)
    {
        this.name = name;
        this.id = ID;
    }
}

export default ModelInfoDTO;