import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-admin-sidebar',
  templateUrl: './admin-sidebar.component.html',
  styleUrls: ['./admin-sidebar.component.css']
})
export class AdminSidebarComponent {
  sidebarOpen = false;

  @Output() navigateEvent = new EventEmitter<string>();

  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
  }

  navigate(route: string) {
    this.navigateEvent.emit(route);
  }
}
