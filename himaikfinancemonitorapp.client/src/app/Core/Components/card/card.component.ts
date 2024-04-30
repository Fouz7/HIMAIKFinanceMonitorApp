import { Component, Input, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrl: './card.component.css',
  encapsulation: ViewEncapsulation.None
})
export class CardComponent {
  @Input() cardTitle: string | undefined;
  @Input() cardContent: string | undefined;
  @Input() imgContent: string | undefined;
  @Input() styling: string | undefined;
}
