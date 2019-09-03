import { Component, OnInit } from '@angular/core';
import { FileUploadService } from '../file-upload.service';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css'],
  providers: [FileUploadService]
})
export class UploadFileComponent implements OnInit {
imageUrl : string = "assets/img/Upload-PNG-Image-File.png";
fileToUpload : File = null;

  constructor(private fileUploadService : FileUploadService) { }

  ngOnInit() {
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
  }

  // I can get rid of fileToUpload and in component also get rid of the function handleFileInput 
  OnSubmit(Caption, File){
    this.fileUploadService.postFile(Caption.value,this.fileToUpload).subscribe(
      data =>{
        console.log('done');
        Caption.value = null;
        File.value = null;
      }
    );
  }

}
