import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FileUploadService } from '../file-upload.service';
import { Router } from '@angular/router';
import { MessageService } from '../message-service.service';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css'],
  providers: [FileUploadService]
})
export class UploadFileComponent implements OnInit {

  @Output() myEvent = new EventEmitter<string>();

  imageUrl: string = "assets/img/Upload-PNG-Image-File.png";
  uplodedDatasetName: string = "";
  statisticsPage: string = "/statistics";
  allDatasetNames: Array<string> = new Array();
  fileToUpload : File = null;

  constructor(private fileUploadService: FileUploadService, 
    private _router: Router, 
    private _messageService: MessageService) {

    this.fileUploadService.getDatasetNames().subscribe(
      data => {
        this.allDatasetNames = data.names
      },
      error => alert(error)
    )
  }

  eventStaisticsRequested(datasetName: string){
    this._messageService.filter(datasetName);
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
  }

  OnSubmit(Caption, File) {
    this.fileUploadService.postFile(Caption.value, this.fileToUpload).subscribe(
      data => {
        this.allDatasetNames.push(<string>(data))
        Caption.value = null;
        File.value = null;
      },
      error => {
        console.log("error with code "+ error)
      }
    );
  }

  ngOnInit() {
  }

}
