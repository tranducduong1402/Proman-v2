import { result } from 'lodash-es';
import { TicketService } from './../../service/api/ticket.service';
import { ColumnStatusService } from './../../service/api/column-status.service';
import { Component, EventEmitter, Inject, Injector, OnInit, Optional, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AppComponentBase } from '../../../shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import * as _ from 'lodash';
import { ProjectService } from '@app/service/api/project.service';

@Component({
  selector: 'app-create-ticket',
  templateUrl: './create-ticket.component.html',
  styleUrls: ['./create-ticket.component.css']
})
export class CreateTicketComponent extends AppComponentBase
implements OnInit {
  @Output() onSave = new EventEmitter<any>();
  saving = false;

  ticket = new CreateTicketDto();
  title: string;
  type: number;

  projectList: any;
  columnStatusList: any;
  selectedValue: any;

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    // public userService: UserService,
    public projectService: ProjectService,
    public columnStatusService: ColumnStatusService,
    public ticketService: TicketService,
  ) {
    super(injector);
  }

  changeProject(e) {
    this.selectedValue = e.target.value;
  }
  changeColumnStatus(e) {
    this.selectedValue = e.target.value;
  }

  getAllColumnStatus() {
    this.columnStatusService.getAllColumnStatusTicketNotPaging().subscribe((data: any) => {
      this.columnStatusList = data.result;
    })
  }

  getAllProject() {
    this.projectService.getAllProjectNotPaging().subscribe((data: any) => {
      this.projectList = data.result;
    })
  }

  ngOnInit(): void {
    this.getAllColumnStatus();
    this.getAllProject();

    if (this.ticket.id == null) {
      this.title = 'Create Ticket';
    }
    else {
      this.title = 'Edit Ticket';
    }
  }

  save() {
    this.ticketService.createTicket(this.ticket).subscribe( 
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
export class CreateTicketDto {
  id: number;
  title: string;
  description: string;
  projectId: number;
  projectName: string;
  columnStatusId: number;
  columnStatusName: string;
}
