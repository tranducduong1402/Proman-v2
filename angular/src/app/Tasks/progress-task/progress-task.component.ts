import { TaskService } from './../../service/api/task.service';
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
  selector: 'app-progress-task',
  templateUrl: './progress-task.component.html',
  styleUrls: ['./progress-task.component.css']
})

export class ProgressTaskComponent extends AppComponentBase
  implements OnInit {
  // saving = false;

  // ticket = new EdiTicketDto();
  // id: number;

  // projectList: any;
  // columnStatusList: any;
  // selectedValue: any;

  // @Output() onSave = new EventEmitter<any>();

  // constructor(
  //   injector: Injector,
  //   public columnStatusService: ColumnStatusService,
  //   public projectService: ProjectService,
  //   public ticketService: TicketService,
  //   public bsModalRef: BsModalRef
  // ) {
  //   super(injector);
  // }
  
  // changeProject(e) {
  //   this.selectedValue = e.target.value;
  // }
  // changeColumnStatus(e) {
  //   this.selectedValue = e.target.value;
  // }

  // getAllColumnStatus() {
  //   this.columnStatusService.getAllColumnStatusTicketNotPaging().subscribe((data: any) => {
  //     this.columnStatusList = data.result;
  //   })
  // }

  // getAllProject() {
  //   this.projectService.getAllProjectNotPaging().subscribe((data: any) => {
  //     this.projectList = data.result;
  //   })
  // }

  // ngOnInit(): void {
    // this.getAllColumnStatus();
    // this.getAllProject();

    // this.ticketService.getOneTicket(this.id).subscribe((data) => {
    //   this.ticket = data.result;
    // });
  // }

  // save(): void {
  //   this.saving = true;

  //   this.ticketService.updateTicket(this.ticket).subscribe(
  //     () => {
  //       this.notify.info(this.l('Saved Successfully'));
  //       this.bsModalRef.hide();
  //       this.onSave.emit();
  //     },
  //     () => {
  //       this.saving = false;
  //     }
  //   );
  // }

  currentItem: any;
  // ticketsArray: any;
  tasksArray: any[];
  // ticketsList: any;

  constructor(
    injector: Injector,
    public taskService: TaskService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.taskService.getAllTaskNotPaging().subscribe((data: any) => {
      this.tasksArray = data.result;
      
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

  // getTickets() {
  //   this.ticketService.getAllTicketNotPaging().subscribe((data: any) => {
  //     this.ticketsList = data.result;

  //     console.log(data.result);
  //   })
  // }
  
  // ticketsArray = this.ticketsList;

  // ticketsArray: any[] = [
  //   {
  //     ticketId: "Ji-001",
  //     ticketName: "Layout Page",
  //     status: "Progress"
  //   }, 
  //   {
  //     ticketId: "Ji-002",
  //     ticketName: "Layout Page2",
  //     status: "Progress"
  //   }, 
  //   {
  //     ticketId: "Ji-003",
  //     ticketName: "Layout Page3",
  //     status: "To Do"
  //   }, 
  //   {
  //     ticketId: "Ji-004",
  //     ticketName: "Layout Page4",
  //     status: "Done"
  //   }, 
  //   {
  //     ticketId: "Ji-005",
  //     ticketName: "Layout Page5",
  //     status: "Progress"
  //   }, 
  //   {
  //     ticketId: "Ji-006",
  //     ticketName: "Layout Page6",
  //     status: "To Do"
  //   }, 
  //   {
  //     ticketId: "Ji-007",
  //     ticketName: "Layout Page7",
  //     status: "To Do"
  //   }, 
  // ]

  filterTasks(status: string) {
    // return this.ticketsArray.filter(m => m.status == status);
    return this.tasksArray.filter(m => m.columnStatusName == status);
  }

  onDragStart(item: any) {
    console.log('onDragStart');
    this.currentItem = item;
  }

  // onDrop(event: any, status: string) {
  //   console.log('onDrop');
  //   event.preventDefault();
  //   const record = this.ticketsArray.find(m => m.title == this.currentItem.title);
  //   if(record != undefined) {
  //     record.status = status;
  //   }
  //   this.currentItem = null;
  // }

  onDragOver(event: any) {
    console.log('onDragOver');
    event.preventDefault();
  }

}
