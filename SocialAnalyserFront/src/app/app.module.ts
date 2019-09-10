import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FileUploadService } from './file-upload.service';
import { UploadFileComponent } from './upload-file/upload-file.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { StatisticsService } from './statistics.service';

@NgModule({
  declarations: [
    AppComponent,
    UploadFileComponent,
    StatisticsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    FileUploadService,
    StatisticsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
