import { AddShoppingList } from './../../models/add-shopping-list';
import { ShoppingListsService } from './../../services/shopping-lists.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  Component,
  Input,
  OnInit,
  ViewChild,
  ViewContainerRef,
  ComponentRef,
} from '@angular/core';
import { ModalConfig } from 'app/shared/components/modals/modal/modal.config';
import { ModalComponent } from 'app/shared/components/modals/modal/modal.component';
import { AddShoppingListProductFormComponent } from '../../forms/add-shopping-list-product-form/add-shopping-list-product-form.component';
import { ShoppingListProduct } from '../../models/shopping-list-product';

@Component({
  selector: 'app-add-shopping-list',
  templateUrl: './add-shopping-list.component.html',
  styleUrls: ['./add-shopping-list.component.scss'],
})
export class AddShoppingListComponent implements OnInit {
  @Input() year!: number;
  @Input() month!: number;
  addShoppingListForm!: FormGroup;

  @ViewChild('container', { read: ViewContainerRef })
  container!: ViewContainerRef;

  productComponents: ComponentRef<AddShoppingListProductFormComponent>[] = [];

  @ViewChild('modal') private modal!: ModalComponent;

  public modalConfig: ModalConfig = {
    modalTitle: 'Add shopping list',
    onSave: () => this.onSave(),
    onClose: () => this.onClose(),
    onDismiss: () => this.onDismiss(),
  };

  constructor(private shoppingListService: ShoppingListsService) {}

  ngOnInit(): void {
    this.addShoppingListForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(20),
      ]),
    });
  }

  openModal(): void {
    this.modal.open();
  }

  onSave(): void {
    if (this.addShoppingListForm.invalid) {
      this.addShoppingListForm.markAllAsTouched();
      this.productComponents.map((p) => {
        if (p.instance.addShoppingListProductForm.invalid) {
          p.instance.addShoppingListProductForm.markAllAsTouched();
        }
      });
      return;
    }

    const name = this.addShoppingListForm.get('name')?.value;

    const products: ShoppingListProduct[] = this.productComponents.map((p) => {
      return p.instance.getProduct();
    });

    const addShoppingList: AddShoppingList = {
      name: name,
      products: products,
    };

    this.shoppingListService.add(addShoppingList).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );

    this.addShoppingListForm.reset();
    this.productComponents.map((x) => x.destroy());

    this.modal.close();
  }

  onClose(): void {
    this.addShoppingListForm.reset();
    this.productComponents.map((x) => x.destroy());
  }

  onDismiss(): void {
    this.addShoppingListForm.reset();
    this.productComponents.map((x) => x.destroy());
  }

  addProduct() {
    const product = this.container.createComponent(
      AddShoppingListProductFormComponent
    );
    product.instance.uniqueKey = this.productComponents.length + 1;
    product.instance.delete.subscribe((uniqueKey: number) => {
      this.removeProduct(uniqueKey);
    });

    this.productComponents.push(product);
  }

  removeProduct(uniqueKey: number) {
    const productComponent: ComponentRef<AddShoppingListProductFormComponent> =
      this.productComponents.find((pc) => pc.instance.uniqueKey == uniqueKey)!;
    const productComponentIndex =
      this.productComponents.indexOf(productComponent);

    if (productComponentIndex !== -1) {
      this.container.remove(this.productComponents.indexOf(productComponent));
      this.productComponents.splice(productComponentIndex, 1);
    }
  }
}
