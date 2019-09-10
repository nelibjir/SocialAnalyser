import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private _listners = new Subject<any>();

  listen(): Observable<any> {
    return this._listners.asObservable();
 }

 filter(filterBy: string) {
    this._listners.next(filterBy);
 }
}
