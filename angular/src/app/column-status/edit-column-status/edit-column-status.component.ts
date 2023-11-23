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
import {
  UserServiceProxy,
  UserDto,
  RoleDto
} from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-edit-column-status',
  templateUrl: './edit-column-status.component.html',
  styleUrls: ['./edit-column-status.component.css']
})

export class EditColumnStatusComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  columnStatus = new EditColumnStatusDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _userService: UserServiceProxy,
    public columnStatusService: ColumnStatusService,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }
  
  

  ngOnInit(): void {
    
    this.columnStatusService.getOneColumnStatus(this.id).subscribe((data) => {
      this.columnStatus = data.result;
      
    });
  }

  save(): void {
    this.saving = true;

    this.columnStatusService.updateColumnStatus(this.columnStatus).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}

export class EditColumnStatusDto {
  id: number;
  name: string;
  type: number;
}