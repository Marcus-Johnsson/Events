import ApiService from "./apiService";

export interface GetAllEventResponse {
    id: number;
    description: string;
    title: string;
    location: string;
    startDate: Date;
    endDate: Date;
    categories?: CategoryCreationResponse[];
}

export interface CategoryCreationResponse { 
  categoryId: number; 
  title: string; 
}


export class EventServiceGet {
    constructor(private apiService: ApiService) {}

    async getAllEvents(): Promise<GetAllEventResponse[]> {
        const response = await this.apiService.get<GetAllEventResponse[]>("events");


        if ("code" in response) {
            throw new Error(response.message);
        }
        return response;
    }
}


