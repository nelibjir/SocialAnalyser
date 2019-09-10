import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface BaseStatistics {
  avgNumberOfFreinds: number,
  numberOfUsers: number
}

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {

  serverRootPath : string = 'https://localhost:44354/api/v1/datasets';

  constructor(private http : HttpClient) { }

  getStatistics(datasetName: string) {
    const endpoint = this.serverRootPath + '/' + datasetName;
    return this.http
      .get<BaseStatistics>(endpoint);
  }
}
