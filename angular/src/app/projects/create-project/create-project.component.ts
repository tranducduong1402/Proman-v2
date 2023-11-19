import { Component, EventEmitter, Inject, Injector, OnInit, Optional, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ProjectService } from '@app/service/api/project.service';
import { UserService } from '@app/service/api/user.service';
import { AppComponentBase } from '@shared/app-component-base';
import { APP_CONSTANT } from '@app/constant/api.constants';
import { BsModalRef } from 'ngx-bootstrap/modal';
import {
  MatDialogModule,
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef
} from '@angular/material/dialog';
import * as _ from 'lodash';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent extends AppComponentBase
implements OnInit {
  @Output() onSave = new EventEmitter<any>();
  saving = false;
  project = { users: []} as ProjectDto;
  projectMembers: ProjectUserDto[] = [];
  displayProjectMembers: ProjectUserDto[] = [];
  title: string;
  isShowTeamMember = false;
  isShowDeactiveMember = false;
  searchProjectMemberText = '';
  statusForFilter;
  activeMembers: UserDto[] = [];
  displayActiveMembers: UserDto[] = [];
  isShowInactiveUser = false;
  searchMemberText = '';
  userTypeForFilter = -1;
  isSaving: boolean = false;

  listCustomer: CustomerDto[] = [];
  customerSearch: FormControl = new FormControl("")
  listCustomerFilter: CustomerDto[];
  defaultRoleCheckedStatus = false;
  respone = 0;

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    public userService: UserService,
    public projectService: ProjectService,
    // public _dialogRef: MatDialogRef<CreateProjectComponent>,
    // public _dialog: MatDialog,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    super(injector);
  }

  ngOnInit(): void {
    //this.project = this.data.project;
    this.getAllMember();
    this.getAllCustomer();
    if (this.project.id == null) {
      this.title = 'Create Project';
    }
    else {
      this.title = 'Edit Project :';
    }
    this.statusForFilter = "";
  }

  isRoleChecked(id: number): boolean {
    // just return default role checked status
    // it's better to use a setting
    return this.defaultRoleCheckedStatus;
  }

  // close(res): void {
  //   this._dialogRef.close(res);
  // }

  showTeamMember(){
    this.isShowTeamMember = !this.isShowTeamMember;
  }

  getAllMember() {
    this.userService.getAllUserNotPaging().subscribe(res => {
      const allMembers = res.result as UserDto[];
      if (this.project.users) {
        this.activeMembers = allMembers.filter(member => member.isActive && !this.project.users.some(s => member.id === s.userId));
      } else this.activeMembers = allMembers.filter(member => member.isActive);
    
      // tslint:disable-next-line: max-line-length
      if (this.project.users && this.project.users.length > 0) {
        this.project.users.map(s => {
          const item = { userId: s.userId, ptype: s.type} as ProjectUserDto;
          const user = allMembers.find(u => u.id === item.userId);
          if (user) {
            item.type = user.type;
            item.isActive = user.isActive;
            item.name = user.name;
            item.avatarFullPath = user.avatarFullPath;
            this.projectMembers.push(item);
            if (this.isShowDeactiveMember || item.ptype != APP_CONSTANT.EnumUserType.DeActive) {
              this.displayProjectMembers.push(item)
            }
          }
        });
      }

      this.displayActiveMembers = this.activeMembers.filter(x => true); 

    })

  }

  onShowDeactiveMemberChange() {
    if (this.isShowDeactiveMember) {

      this.displayProjectMembers = this.projectMembers.slice().filter(
        member => ((member.name.toLowerCase().includes(this.searchProjectMemberText.toLowerCase()))
          || (member.emailAddress.toLowerCase().includes(this.searchProjectMemberText.toLowerCase()))
        )
      )

    } else {

      this.displayProjectMembers = this.projectMembers.filter(
        member => member.ptype !== APP_CONSTANT.EnumUserType.DeActive).filter(
          member => ((member.name.toLowerCase().includes(this.searchProjectMemberText.toLowerCase()))
            || (member.emailAddress.toLowerCase().includes(this.searchProjectMemberText.toLowerCase()))
          )
        )
    }
  }

  filterProjectMember() {
    this.displayProjectMembers = this.projectMembers.filter(
      member => (((member.name.toLowerCase().includes(this.searchProjectMemberText.toLowerCase()))
        || (member.emailAddress.toLowerCase().includes(this.searchProjectMemberText.toLowerCase()))))
    );

    if (this.isShowInactiveUser && this.isShowDeactiveMember) {
      this.displayProjectMembers = this.displayProjectMembers.filter(
        member => (
          member.isActive === APP_CONSTANT.EnumUserStatus.InActive
          && member.ptype === APP_CONSTANT.EnumUserType.DeActive)
      );
      return;
    }

    if (this.isShowInactiveUser) {
      this.displayProjectMembers = this.displayProjectMembers.filter(
        member => (
          member.isActive === APP_CONSTANT.EnumUserStatus.InActive)
      );
      return;
    }

    if (this.isShowDeactiveMember) {
      this.displayProjectMembers = this.displayProjectMembers.filter(
        member => (member.ptype === APP_CONSTANT.EnumUserType.DeActive)
      );
      return;
    }

    this.displayProjectMembers = this.displayProjectMembers.filter(
      member => (member.ptype !== APP_CONSTANT.EnumUserType.DeActive)
    );
    return;
  }

  removeMemberFromProject(member: ProjectUserDto, index) {
    // this.projectMembers.splice(index, 1);
    this.displayProjectMembers.splice(index, 1);
    this.projectMembers.splice(this.projectMembers.findIndex(s => s.userId === member.userId), 1);
    const { userId, isActive, name, type, avatarFullPath, emailAddress } = member;
    const u = {
      id: userId,
      isActive,
      name,
      type,
      avatarFullPath,
      emailAddress,
    } as UserDto;
    this.activeMembers.push(u);
    this.displayActiveMembers.push(u);
  }

  searchMember() {
    this.displayActiveMembers = this.activeMembers.filter(
      member => (!this.searchMemberText || (member.emailAddress.toLowerCase().includes(this.searchMemberText.toLowerCase())) 
      || member.name.search(new RegExp(this.searchMemberText, 'ig')) > -1)
        && (this.userTypeForFilter < 0 || member.type === this.userTypeForFilter)
    );
  }

  searchProjectMember() {
    this.displayProjectMembers = this.projectMembers.filter(
      member => (((member.name.toLowerCase().includes(this.searchProjectMemberText.toLowerCase()))
        || (member.emailAddress.toLowerCase().includes(this.searchProjectMemberText.toLowerCase())))
        && (!this.isShowDeactiveMember ? member.ptype !== APP_CONSTANT.EnumUserType.DeActive : true))
    );
  }

  selectTeam(user: UserDto, index) {
    this.displayActiveMembers.splice(index, 1);
    const member = {
      userId: user.id,
      isActive: user.isActive,
      name: user.name,
      type: user.type,
      avatarFullPath: user.avatarFullPath,
      ptype: this.projectMembers.length == 0 ? APP_CONSTANT.EnumUserType.PM : APP_CONSTANT.EnumUserType.Member,
      emailAddress: user.emailAddress,
    } as ProjectUserDto;

    this.projectMembers.push(member);
    this.displayProjectMembers.push(member)
    this.activeMembers.splice(this.activeMembers.findIndex(s => s.id === user.id), 1);
  }

  save() {
    this.project.users = this.projectMembers.map((member) => ({
      userId: member.userId,
      type: member.ptype,
    }));
    if (_.isEmpty(this.projectMembers)) {
      abp.message.error("Project must have at least one member!")
      return;
    }
    if (!this.projectMembers.some(x => x.ptype === APP_CONSTANT.EnumUserType.PM)) {
      abp.message.error("Project must have a PM!")
      return;
    }
    //this.project.status = APP_CONSTANT.EnumProjectStatus.Active;

    this.isSaving = true;
    this.projectService.createProject(this.project).subscribe(res => {
      this.isSaving = false;
      if (res.success == true) {
        if (this.project.id == null) {
          this.notify.success(this.l('Create Project Successfully'));
          this.bsModalRef.hide();
        this.onSave.emit();
        }
        else {
          this.notify.success(this.l('Update Project Successfully'));
          this.bsModalRef.hide();
        this.onSave.emit();
        }
        this.respone = 1;
        // this.close(this.respone);
      }
    }, err => {
      this.isSaving = false;
    })

  }

  getAllCustomer() {
    this.userService.getAllClientNotPaging().subscribe(res => {
      this.listCustomer = res.result;
      this.listCustomerFilter = this.listCustomer;
    })
  }

  filterCustomer(): void {
    if (this.customerSearch.value) {
      this.listCustomer = this.listCustomerFilter.filter(data => data.name.toLowerCase().includes(this.customerSearch.value.toLowerCase().trim()));
    } else {
      this.listCustomer = this.listCustomerFilter.slice();
    }
  }
}

export class CreateProjectDto {
  customerId: number;
  id: number;
  name: string;
  code: string;
  note: string;
  isActive: boolean;
  timeStart: Date;
  timeEnd: Date;
}

export class CustomerDto {
  id: number;
  name: string;
  code: string;
  address: string;
}

export class BaseUserDto {
  name: string;
  isActive: boolean;
  emailAddress: string;
  type: number;
  avatarPath: string;
  avatarFullPath: string;
}

export class UserDto extends BaseUserDto {
  id: number;
}

export interface GetProjectDto {
  id: number;
  name: string;
  code: string;
  status: number;
  customerName: string;
}

export interface ProjectDto {
  id: number;
  name: string;
  code: string;
  status: number;
  note: string;
  timeStart: Date;
  timeEnd: Date;
  customerId: number;
  users: UserProjectDto[];
}

export interface UserProjectDto {
  userId: number,
  type: number,
  id?: number
}

export class ProjectUserDto extends BaseUserDto {
  userId: number;
  ptype: number;
}