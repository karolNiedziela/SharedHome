import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import {
  faChevronLeft,
  faChevronRight,
  faPlus,
} from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { CalendarView, DAYS_OF_WEEK } from 'angular-calendar';
import { endOfDay, startOfDay } from 'date-fns';
import { map, Observable, Subject, Subscription } from 'rxjs';
import { ApiResponse } from 'src/app/core/models/api-response';
import { PopupMenuConfig } from 'src/app/shared/components/menus/popup-menu/popup-menu.config';
import { ConfirmationModalComponent } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalConfig } from 'src/app/shared/components/modals/confirmation-modal/confirmation-modal.config';
import { CellPipeFormat } from 'src/app/shared/components/tables/cell-pipe-format';
import { TableColumn } from 'src/app/shared/components/tables/column-setting';
import { BillType } from '../enums/bill-type';
import { CancelBillPaymentComponent } from '../modals/cancel-bill-payment/cancel-bill-payment.component';
import { EditBillComponent } from '../modals/edit-bill/edit-bill.component';
import { PayForBillComponent } from '../modals/pay-for-bill/pay-for-bill.component';
import { Bill } from '../models/bill';
import { BillEvent } from '../models/bill-event';
import { BillService } from '../services/bill.service';

@Component({
  selector: 'app-bills-calendar',
  templateUrl: './bills-calendar.component.html',
  styleUrls: ['./bills-calendar.component.scss'],
})
export class BillsCalendarComponent implements OnInit, OnDestroy {
  addIcon = faPlus;
  previousMonthIcon = faChevronLeft;
  nextMonthIcon = faChevronRight;

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

  activeDayIsOpen: boolean = true;

  billTableColumns: TableColumn[] = [
    {
      name: 'Service Provider',
      dataKey: 'serviceProvider',
    },
    {
      name: 'Date of Payment',
      dataKey: 'dateOfPayment',
      format: CellPipeFormat.DATE,
    },
    {
      dataKey: 'billType',
      name: 'Type',
      format: CellPipeFormat.ENUM,
      enumType: BillType,
    },
    {
      dataKey: 'cost',
      name: 'Cost',
      format: CellPipeFormat.MONEY,
    },
  ];

  popupMenuConfigs: PopupMenuConfig[] = [];

  @ViewChild('deleteBillModal')
  deleteBillModal!: ConfirmationModalComponent;
  deleteBillModalConfig!: ConfirmationModalConfig;

  @ViewChild('payForBillModal')
  payForBillModal!: PayForBillComponent;

  @ViewChild('cancelBillPaymentModal')
  cancelBillPaymentModal!: CancelBillPaymentComponent;

  @ViewChild('editBillModal')
  edtiBillModal!: EditBillComponent;

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

    this.deleteBillModalConfig = {
      modalTitle: 'Delete bill',
      onConfirm: () => {},
    };
  }
  ngOnDestroy(): void {
    this.eventsSubscription.unsubscribe();
  }

  private getBillEvents(selectedDate?: Date): void {
    const year = selectedDate?.getFullYear() ?? new Date().getFullYear();
    const month = selectedDate?.getMonth() ?? new Date().getMonth();
    this.events$ = this.billService
      .getAllByYearAndMonthAndIsDone(year, month + 1)
      .pipe(
        map((response: ApiResponse<Bill[]>) => {
          const billEvents: BillEvent[] = response.data.map((bill: Bill) => {
            return this.getBillEvent(bill);
          });

          this.getPopupMenuConfigs(billEvents);

          if (selectedDate) this.dayClicked(selectedDate, billEvents);

          return billEvents;
        })
      );

    this.refresh.next();
  }

  private getBillEvent(bill: Bill): BillEvent {
    endOfDay(Date.parse(bill.dateOfPayment.toString()));
    const billEvent: BillEvent = {
      id: bill.id,
      start: new Date(startOfDay(Date.parse(bill.dateOfPayment.toString()))),
      end: new Date(endOfDay(Date.parse(bill.dateOfPayment.toString()))),

      title: bill.serviceProvider,
      isPaid: bill.isPaid,
      serviceProvider: bill.serviceProvider,
      dateOfPayment: bill.dateOfPayment,
      billType: bill.billType,
      createdBy: bill.createdBy,
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
      (x) => x.start >= startOfDay(date) && x.end! <= endOfDay(date)
    );

    this.dateSelected = date;

    this.getPopupMenuConfigs(this.currentDayEvents);
  }

  getBillTypes(billEvents: BillEvent[]): BillType[] {
    return billEvents.map((billEvent) => billEvent.billType);
  }

  getPopupMenuConfigs(billEvents: BillEvent[]): void {
    const popupMenuConfigs: PopupMenuConfig[] = [];

    billEvents.forEach((billEvent: BillEvent) => {
      const popupMenuConfig: PopupMenuConfig = {
        isDeleteVisible: true,
        onDelete: () => {
          this.deleteBillModalConfig.onConfirm = () => {
            this.billService.deleteBill(billEvent.id?.toString()!).subscribe();
          };
          this.deleteBillModal.open();
        },
        isEditVisible: true,
        onEdit: () => {
          this.edtiBillModal.bill = billEvent;
          this.edtiBillModal.openModal();
        },
      };
      if (billEvent.isPaid) {
        popupMenuConfig.additionalPopupMenuItems = [
          {
            text: 'Cancel payment',
            onClick: () => {
              this.cancelBillPaymentModal.billId = billEvent.id!.toString();
              this.cancelBillPaymentModal.openModal();
            },
          },
        ];
      } else {
        popupMenuConfig.additionalPopupMenuItems = [
          {
            text: 'Pay',
            onClick: () => {
              this.payForBillModal.billId = billEvent.id?.toString()!;
              this.payForBillModal.openModal();
            },
          },
        ];
      }

      popupMenuConfigs.push(popupMenuConfig);
    });

    this.popupMenuConfigs =
      popupMenuConfigs.length == 0 ? [] : popupMenuConfigs;
  }

  onMonthChanged(date: Date): void {
    this.getBillEvents(date);
    this.refresh.next();
  }
}
