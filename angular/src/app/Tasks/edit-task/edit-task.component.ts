import { TaskService } from './../../service/api/task.service';
import { UserService } from './../../service/api/user.service';
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

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css']
})

export class EditTaskComponent extends AppComponentBase
  implements OnInit {
  saving = false;

  task = new EdiTaskDto();
  id: number;

  // projectList: any;
  userList: any;
  ticketList: any;
  columnStatusList: any;
  selectedValue: any;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public columnStatusService: ColumnStatusService,
    public taskService: TaskService,
    public ticketService: TicketService,
    public userService: UserService,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }
  
  changeTicket(e) {
    this.selectedValue = e.target.value;
  }

  changeUser(e) {
    this.selectedValue = e.target.value;
  }

  changeColumnStatus(e) {
    this.selectedValue = e.target.value;
  }

  getAllColumnStatus() {
    this.columnStatusService.getAllColumnStatusTaskNotPaging().subscribe((data: any) => {
      this.columnStatusList = data.result;
    })
  }

  getAllTicket() {
    this.ticketService.getAllTicketNotPaging().subscribe((data: any) => {
      this.ticketList = data.result;
    })
  }

  getAllUser() {
    this.userService.getAllUserNotPaging().subscribe((data: any) => {
      this.userList = data.result;
    })
    console.log(this.userList);
    
  }

  ngOnInit(): void {
    this.getAllColumnStatus();
    this.getAllTicket();
    this.getAllUser();

    this.taskService.getOneTask(this.id).subscribe((data) => {
      this.task = data.result;
    });
  }

  save(): void {
    this.saving = true;

    this.taskService.updateTask(this.task).subscribe(
      () => {
        this.notify.info(this.l('Saved Successfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}

export class EdiTaskDto {
  id: number;
  title: string;
  description: string;

  columnStatusName: string; 
  columnStatusId: number;

  ticketTitle: string;
  ticketId: number;

  userName: string;
  userId: number;
}