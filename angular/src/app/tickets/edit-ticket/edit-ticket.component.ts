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
import { ProjectService } from '@app/service/api/project.service';

@Component({
  selector: 'app-edit-ticket',
  templateUrl: './edit-ticket.component.html',
  styleUrls: ['./edit-ticket.component.css']
})

export class EditTicketComponent extends AppComponentBase
  implements OnInit {
  saving = false;

  ticket = new EdiTicketDto();
  id: number;

  projectList: any;
  columnStatusList: any;
  selectedValue: any;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public columnStatusService: ColumnStatusService,
    public projectService: ProjectService,
    public ticketService: TicketService,
    public bsModalRef: BsModalRef
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

    this.ticketService.getOneTicket(this.id).subscribe((data) => {
      this.ticket = data.result;
    });
  }

  save(): void {
    this.saving = true;

    this.ticketService.updateTicket(this.ticket).subscribe(
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

export class EdiTicketDto {
  id: number;
  title: string;
  description: string;
  columnStatusName: string; 
  projectName: string;
  projectId: number;
  columnStatusId: number;
}