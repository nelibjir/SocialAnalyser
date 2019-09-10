import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface Datasets {
  names: string[] 
}

@Injectable({
  providedIn: 'root'
})

export class FileUploadService {
  constructor(private http : HttpClient) { }

  serverRootPath : string = 'https://localhost:44354/api/v1/datasets';

  postFile(caption: string, fileToUpload: File) {
    const formData: FormData = new FormData();
    formData.append('Dataset', fileToUpload, fileToUpload.name);
    formData.append('Name', caption);
    return this.http
      .post(this.serverRootPath, formData);
  }

  getDatasetNames() {
    return this.http
      .get<Datasets>(this.serverRootPath);
  }
}
