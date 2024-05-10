import { Directive, ElementRef, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Directive({
  selector: '[appHeaderPosition]'
})
export class HeaderPositionDirective implements OnInit {

  constructor(private el: ElementRef, private router: Router) { }

  ngOnInit() {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        if (event.urlAfterRedirects.includes('/admin-dashboard')) {
          this.el.nativeElement.classList.remove('fixed-header');
          this.el.nativeElement.classList.add('relative-header');
        } else {
          this.el.nativeElement.classList.remove('relative-header');
          this.el.nativeElement.classList.add('fixed-header');
        }
      }
    });
  }
}
