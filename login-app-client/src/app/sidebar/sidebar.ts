import { Component, EventEmitter, Output } from '@angular/core';
import { SidebarService } from '../services/sidebar-service';


@Component({
  standalone: true,
  selector: 'app-sidebar',
  imports: [],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss'
})
export class Sidebar {
constructor(private sideBarService: SidebarService){}
isOpen = false;
userName : string = '';
  @Output() sidebarOpen = new EventEmitter<boolean>();

ngOnInit(){
 this.userName = localStorage.getItem('Username') ?? '' ;
}

toogleSideBar(){
this.isOpen = !this.isOpen;
    this.sidebarOpen.emit(this.isOpen);
    this.sideBarService.setSidebar(this.isOpen);
    console.log(this.isOpen);
}
}
