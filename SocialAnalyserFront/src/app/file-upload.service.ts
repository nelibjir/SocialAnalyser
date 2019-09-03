import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  constructor(private http : HttpClient) { }

  postFile(caption: string, fileToUpload: File) {
    const endpoint = 'https://localhost:44354/api/v1/datasets';
    const formData: FormData = new FormData();
    formData.append('Dataset', fileToUpload, fileToUpload.name);
    formData.append('Name', caption);
    return this.http
      .post(endpoint, formData);
  }
  
}
