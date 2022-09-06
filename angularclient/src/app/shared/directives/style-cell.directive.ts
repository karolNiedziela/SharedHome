import { Directive, Input, ElementRef, Renderer2, OnInit } from '@angular/core';

@Directive({
  selector: '[appStyleCell]',
})
export class StyleCellDirective implements OnInit {
  @Input() appStyleCell: any;
  @Input() key!: string;

  constructor(private elementRef: ElementRef, private renderer: Renderer2) {}

  ngOnInit(): void {
    if (this.appStyleCell === undefined) {
    }

    if (typeof this.appStyleCell === 'number') {
    }

    if (this.key === 'isPaid') {
      if (this.appStyleCell === true) {
        this.renderer.setStyle(
          this.elementRef.nativeElement,
          'color',
          '#13a500'
        );

        this.elementRef.nativeElement.innerHTML = 'Yes';
      }

      if (this.appStyleCell === false) {
        this.renderer.setStyle(
          this.elementRef.nativeElement,
          'color',
          '#c9ae1c'
        );
        this.elementRef.nativeElement.innerHTML = 'No';
      }
    }

    // if (
    //   typeof this.appStyleCell === 'object' &&
    //   this.moneyPropertyNames.some((x) => x == this.key)
    // ) {
    //   this.elementRef.nativeElement.innerHTML = `${this.appStyleCell.price} ${this.appStyleCell.currency}`;
    // }
  }
}
