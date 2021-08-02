import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Order } from 'src/app/models/order';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-ordertracking',
  templateUrl: './ordertracking.component.html',
  styleUrls: ['./ordertracking.component.css']
})
export class OrdertrackingComponent implements OnInit, OnDestroy {

  id: any;
  order: Order;
  guid: FormControl = new FormControl('', [Validators.required, Validators.minLength(32)])

  constructor(private route: ActivatedRoute, private orderService: OrderService, private router: Router) { }

  ngOnInit(): void {
    const guid = this.route.snapshot.paramMap.get('guid');
    if (guid && guid.length === 36) {
      this.guid.setValue(guid);
      this.orderService.findByGuid(guid).subscribe((order: Order) => {
        this.order = order    
      });
      this.id = setInterval(() => {
        this.orderService.findByGuid(guid).subscribe((order: Order) => {
          this.order = order    
        },
          error => console.log(error));
      }, 10000)
    }
  }

  ngOnDestroy() {
    if (this.id) {
      clearInterval(this.id);
    }
  }

  get trackingButtonDisabled(): boolean {
    return !this.guid.valid;
  }

  get orderTotal() {
    return this.order.orderItems.reduce((total, orderItem) => {
      return total += orderItem.item.price * orderItem.quantity;
    }, 0.00).toFixed(2);
  }

  getOrderStatus() {
    this.router.navigate(['/orderstatus', this.guid.value])
      .then(() => window.location.reload());
  }

}
