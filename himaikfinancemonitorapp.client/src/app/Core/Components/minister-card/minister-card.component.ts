import { Component, Input, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-minister-card',
  templateUrl: './minister-card.component.html',
  styleUrl: './minister-card.component.css'
})
export class MinisterCardComponent {
  @Input() cardTitle: string | undefined;
  @Input() cardContent: string | undefined;
  @Input() cardContent2: string | undefined;
  @Input() imgContent: string | undefined;
}
