import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ProjectService } from '@app/service/api/project.service';
import { UserService } from '@app/service/api/user.service';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent extends AppComponentBase
implements OnInit {
  @Output() onSave = new EventEmitter<any>();
  saving = false;
  project: CreateProjectDto;

  listCustomer: CustomerDto[] = [];
  customerSearch: FormControl = new FormControl("")
  listCustomerFilter: CustomerDto[];

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    public userService: UserService,
    public projectService: ProjectService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.project.isActive = true;
  }

  save(): void {
    this.saving = true;

    this.projectService.create(this.project).subscribe(
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

export class CreateProjectDto {
  customerId: number;
  id: number;
  name: string;
  code: string;
  note: string;
  isActive: boolean;
}

export class CustomerDto {
  id: number;
  name: string;
  code: string;
  address: string;
}
