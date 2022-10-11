import { TranslateService } from '@ngx-translate/core';
import { CancelBillPaymentComponent } from './modals/cancel-bill-payment/cancel-bill-payment.component';
import { ModalConfig } from './../../shared/components/modals/modal/modal.config';
import { ConfirmationModalConfig } from './../../shared/components/modals/confirmation-modal/confirmation-modal.config';
import { ConfirmationModalComponent } from './../../shared/components/modals/confirmation-modal/confirmation-modal.component';
import { Subscription } from 'rxjs/internal/Subscription';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { PopupMenuConfig } from './../../shared/components/menus/popup-menu/popup-menu.config';
import { ColumnSetting } from './../../shared/components/tables/column-setting';
import { BillEvent } from './models/bill-event';
import { ApiResponse } from 'app/core/models/api-response';
import { BillService } from './services/bill.service';
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import {
  CalendarDateFormatter,
  CalendarView,
  DAYS_OF_WEEK,
} from 'angular-calendar';
import { Subject, Observable, map, of } from 'rxjs';
import { startOfDay, endOfDay, isSameDay, isSameMonth } from 'date-fns';
import { Bill } from './models/bill';
import { CellPipeFormat } from 'app/shared/components/tables/cell-pipe-format';
import { BillType } from './enums/bill-type';
import { PayForBillComponent } from './modals/pay-for-bill/pay-for-bill.component';

@Component({
  selector: 'app-bills',
  templateUrl: './bills.component.html',
  styleUrls: ['./bills.component.scss'],
})
export class BillsComponent implements OnInit, OnDestroy {
  addIcon = faPlus;

  events$!: Observable<BillEvent[]>;
  eventsSubscription!: Subscription;

  view: CalendarView = CalendarView.Month;

  currentDayEvents: BillEvent[] = [];

  dateSelected!: Date;

  CalendarView = CalendarView;
  locale!: string;

  viewDate: Date = new Date();

  weekStartsOn: number = DAYS_OF_WEEK.MONDAY;

  refresh = new Subject<void>();

  activeDayIsOpen: boolean = false;

  columnSettings: ColumnSetting[] = [
    {
      propertyName: 'serviceProvider',
      header: 'Service Provider',
    },
    {
      propertyName: 'dateOfPayment',
      header: 'Date of Payment',
      format: CellPipeFormat.DATE,
    },
    {
      propertyName: 'billType',
      header: 'Type',
      format: CellPipeFormat.ENUM,
      enumType: BillType,
    },
    {
      propertyName: 'cost',
      header: 'Cost',
      format: CellPipeFormat.MONEY,
    },
    {
      propertyName: 'isPaid',
      header: 'Paid',
    },
  ];

  @ViewChild('deleteBillModal')
  deleteBillModal!: ConfirmationModalComponent;
  deleteBillModalConfig: ConfirmationModalConfig = {
    modalTitle: 'Delete bill',
  };

  @ViewChild('payForBillModal')
  payForBillModal!: PayForBillComponent;

  @ViewChild('cancelBillPaymentModal')
  cancelBillPaymentModal!: CancelBillPaymentComponent;

  constructor(
    private billService: BillService,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.locale = this.translateService.currentLang;

    this.getBillEvents();

    this.eventsSubscription = this.billService.allBillsRefreshNeeded.subscribe(
      () => {
        this.getBillEvents(this.dateSelected);
      }
    );
  }

  ngOnDestroy(): void {
    this.eventsSubscription.unsubscribe();
  }

  private getBillEvents(selectedDate?: Date): void {
    this.events$ = this.billService.getAllByYearAndMonthAndIsDone(2022, 9).pipe(
      map((response: ApiResponse<Bill[]>) => {
        const billEvents: BillEvent[] = response.data.map((bill: Bill) => {
          return this.getBillEvent(bill);
        });

        if (selectedDate) this.dayClicked(selectedDate, billEvents);
        return billEvents;
      })
    );
  }

  private getBillEvent(bill: Bill): BillEvent {
    const billEvent: BillEvent = {
      id: bill.id,
      start: new Date(Date.parse(bill.dateOfPayment.toString())),
      end: new Date(Date.parse(bill.dateOfPayment.toString())),
      title: bill.serviceProvider,
      isPaid: bill.isPaid,
      serviceProvider: bill.serviceProvider,
      dateOfPayment: bill.dateOfPayment,
      billType: bill.billType,
      cost:
        bill.cost == null
          ? null
          : {
              price: bill.cost.price,
              currency: bill.cost.currency,
            },
    };

    return billEvent;
  }

  dayClicked(date: Date, events: BillEvent[]): void {
    this.currentDayEvents = events.filter(
      (x) => x.start >= startOfDay(date) && x.end! < endOfDay(date)
    );

    this.dateSelected = date;

    this.getPopupMenuConfigs(this.currentDayEvents);
  }

  getBillTypes(billEvents: BillEvent[]): BillType[] {
    return billEvents.map((billEvent) => billEvent.billType);
  }

  getPopupMenuConfigs(billEvents: BillEvent[]): PopupMenuConfig[] {
    const popupMenuConfigs: PopupMenuConfig[] = [];

    billEvents.forEach((billEvent) => {
      const popupMenuConfig: PopupMenuConfig = {
        isDeleteVisible: true,
        onDelete: () => {
          this.deleteBillModalConfig.onSave = () => {
            this.billService.deleteBill(+billEvent.id!).subscribe();
          };
          this.deleteBillModal.open();
        },
        isEditVisible: true,
      };
      if (billEvent.isPaid) {
        popupMenuConfig.additionalPopupMenuItems = [
          {
            text: 'Cancel payment',
            onClick: () => {
              this.cancelBillPaymentModal.billId = +billEvent.id!;
              this.cancelBillPaymentModal.openModal();
            },
          },
        ];
      } else {
        popupMenuConfig.additionalPopupMenuItems = [
          {
            text: 'Pay',
            onClick: () => {
              this.payForBillModal.billId = +billEvent.id!;
              this.payForBillModal.openModal();
            },
          },
        ];
      }

      popupMenuConfigs.push(popupMenuConfig);
    });

    return popupMenuConfigs;
  }

  onMonthChanged(): void {
    this.currentDayEvents = [];
  }
}
