import { Component, OnInit, Input } from '@angular/core';
import { StatisticsService } from '../statistics.service';
import { MessageService } from '../message-service.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css'],
  providers: [StatisticsService]
})
export class StatisticsComponent implements OnInit {

  avgFriends: number;
  numOfFriends: number;
  datasetName: string;

  constructor(private statisticsService: StatisticsService,
    private _messageService: MessageService) {
    this._messageService.listen().subscribe((m: any) => {
      console.log(m);
      this.getStatistics(m);
    })
  }

  getStatistics(datasetName: string) {
    this.statisticsService.getStatistics(datasetName).subscribe(
      data => {
        this.avgFriends = data.avgNumberOfFreinds;
        this.numOfFriends = data.numberOfUsers;
        this.datasetName = datasetName;
      },
      error => alert(error)
    )
  }

  ngOnInit() {
  }

}
