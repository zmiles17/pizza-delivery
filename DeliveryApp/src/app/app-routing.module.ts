import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DragDropComponent } from './components/drag-drop/drag-drop.component';
import { HomeComponent } from './components/home/home.component';
import { OrderStepperComponent } from './components/order-stepper/order-stepper.component';
import { OrdertrackingComponent } from './components/ordertracking/ordertracking.component';
import { TableComponent } from './components/table/table.component';
import { TreeComponent } from './components/tree/tree.component';

const routes: Routes = [
  {path: '*', component: AppComponent},
  {path: '', component: HomeComponent},
  {path: 'placeorder', component: OrderStepperComponent},
  {path: 'orderstatus', component: OrdertrackingComponent},
  {path: 'orderstatus/:guid', component: OrdertrackingComponent},
  {path: 'dashboard', component: DashboardComponent},
  {path: 'table', component: TableComponent},
  {path: 'tree', component: TreeComponent},
  {path: 'dragndrop', component: DragDropComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
