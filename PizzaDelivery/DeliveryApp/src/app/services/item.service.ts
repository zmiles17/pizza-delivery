import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Item } from '../models/item';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  private baseUrl: string = "http://localhost:7022/api/item"

  private headers: object = {
    "Content-Type": "application/json"
  }

  constructor(private http: HttpClient) { }

  getItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this.baseUrl, this.headers);
  }
}
