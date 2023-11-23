import { TaskService } from './../../service/api/task.service';
import { UserService } from '../../service/api/user.service';
import { result } from 'lodash-es';
import { TicketService } from './../../service/api/ticket.service';
import { ColumnStatusService } from './../../service/api/column-status.service';
import { Component, EventEmitter, Inject, Injector, OnInit, Optional, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AppComponentBase } from '../../../shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import * as _ from 'lodash';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent extends AppComponentBase
implements OnInit {
  @Output() onSave = new EventEmitter<any>();
  saving = false;

  task = new CreateTaskDto();
  title: string;
  type: number;

  userList: any;
  ticketList: any;
  columnStatusList: any;
  selectedValue: any;

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    // public userService: UserService,
    public userService: UserService,
    public columnStatusService: ColumnStatusService,
    public ticketService: TicketService,
    public taskService: TaskService,
  ) {
    super(injector);
  }

  changeUser(e) {
    this.selectedValue = e.target.value;
  }

  changeTicket(e) {
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
  }

  ngOnInit(): void {
    this.getAllColumnStatus();
    this.getAllTicket();
    this.getAllUser();

    if (this.task.id == null) {
      this.title = 'Create Task';
    }
    else {
      this.title = 'Edit Task';
    }
  }

  save() {
    this.taskService.createTask(this.task).subscribe( 
      () => {
      this.notify.info(this.l('Save Successfully'));
      this.bsModalRef.hide();
      this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    )

  }

}

export class CreateTaskDto {
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
