import { state } from '@angular/animations';
import { filter } from 'rxjs/operators';
import { TicketService } from './../../service/api/ticket.service';
import { ColumnStatusService } from './../../service/api/column-status.service';
import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map, result } from 'lodash-es';
import { AppComponentBase } from '../../../shared/app-component-base';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
// import { ProjectService } from '@app/service/api/project.service';

@Component({
  selector: 'app-progress-ticket',
  templateUrl: './progress-ticket.component.html',
  styleUrls: ['./progress-ticket.component.css']
})

export class ProgressTicketComponent extends AppComponentBase
  implements OnInit {
  
  currentItem: any;
  ticketsArray: any;
  ticketsList: any;

  constructor(
    injector: Injector,
    public ticketService: TicketService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.ticketService.getAllTicketNotPaging().subscribe((data: any) => {
      this.ticketsArray = data.result;
      
      console.log(data.result);
    })
  }

  onDrop(event: CdkDragDrop<string []>) {

    // event.preventDefault();
    // const record = this.ticketsArray.find(m => m.title == this.currentItem.title);
    // if(record != undefined) {
    //   record.status = status;
    // }
    // this.currentItem = null;

    if(event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }

  

  filterTickets(status: string) {
    // return this.ticketsArray.filter(m => m.status == status);
    return this.ticketsArray.filter(m => m.columnStatusName == status);
  }

  onDragStart(item: any) {
    console.log('onDragStart');
    this.currentItem = item;
  }
  
  onDragOver(event: any) {
    console.log('onDragOver');
    event.preventDefault();
  }

}
