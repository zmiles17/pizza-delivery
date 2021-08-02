import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  images: string[] = [
    "https://cdn11.bigcommerce.com/s-p6vajvx5jy/images/stencil/1280x1280/products/117/663/Meat_Lovers_2__84120.1556128972.jpg?c=2",
    "https://cookingformysoul.com/wp-content/uploads/2021/04/homemade-hawaiian-pizza-1200-min.jpg",
    "https://thumbor.thedailymeal.com/UiBJwQklx0vDGy6x0D0agXQF5ok=//https://www.thedailymeal.com/sites/default/files/2018/04/10/hero_1_0.jpg",
    "https://tmbidigitalassetsazure.blob.core.windows.net/rms3-prod/attachments/37/1200x1200/Grilled-Veggie-Pizza_EXPS_LSBZ18_48960_D01_18_6b.jpg"
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
