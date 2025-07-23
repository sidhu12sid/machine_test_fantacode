import { Component } from '@angular/core';
import { ChartConfiguration, ChartData } from 'chart.js';
import { NgChartsModule } from 'ng2-charts';
import { Subscription } from 'rxjs';
import { SidebarService } from '../services/sidebar-service';


@Component({
  selector: 'app-dashboard',
  imports: [NgChartsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard   {
userName : string = '';
sidebarOpen = false;
private sub!: Subscription;

constructor(private sidebarService : SidebarService){};

ngOnInit(){
  this.userName = localStorage.getItem('Username') ?? '';
  console.log(localStorage.getItem('Username'));

  this.sub = this.sidebarService.isOpen$.subscribe(open => {
      this.sidebarOpen = open;
    });
    console.log('dash',this.sidebarOpen,this.sub);
}

 public barChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: true,
  };

  public barChartData: ChartData<'bar'> = {
    labels: ['Jan', 'Feb', 'Mar', 'Apr'],
    datasets: [
      { data: [50, 49, 35, 62], label: 'Sales' }
    ]
  };

   ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
