import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '../models/store';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  private baseUrl: string = "http://localhost:7022/api/store"
  private headers: object = {
    "Content-Type": "application/json"
  }

  constructor(private http: HttpClient) { }

  getStores(): Observable<Store[]> {
    return this.http.get<Store[]>(this.baseUrl, this.headers);
  }

  getStoreById(id): Observable<Store> {
    return this.http.get<Store>(this.baseUrl + `/${id}`, this.headers);
  }
}
