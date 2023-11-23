import { ColumnStatusService } from './../../service/api/column-status.service';
import { Component, EventEmitter, Inject, Injector, OnInit, Optional, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AppComponentBase } from '../../../shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import {
  MatDialogModule,
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef
} from '@angular/material/dialog';
import * as _ from 'lodash';

@Component({
  selector: 'app-create-column-status',
  templateUrl: './create-column-status.component.html',
  styleUrls: ['./create-column-status.component.css']
})
export class CreateColumnStatusComponent extends AppComponentBase
implements OnInit {
  @Output() onSave = new EventEmitter<any>();
  saving = false;

  columnStatus = new CreateColumnStatusDto();
  name: string;
  type: number;
  
  defaultRoleCheckedStatus = false;

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    public columnStatusService: ColumnStatusService,
    
  ) {
    super(injector);
  }

  ngOnInit(): void {

    if (this.columnStatus.id == null) {
      this.name = 'Create Column Status';
    }
    else {
      this.name = 'Edit Column Status';
    }
  }

  save() {

    this.columnStatusService.createColumnStatus(this.columnStatus).subscribe( 
      () => {
      this.notify.info(this.l('SaveSuccessfully'));
      this.bsModalRef.hide();
      this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    )

  }
  
}

export class CreateColumnStatusDto {
  id: number;
  name: string;
  type: number;
}
