import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ProjectsComponent } from './projects/projects.component';
import { ClientsComponent } from './clients/clients.component';
import { ColumnStatusesComponent } from './column-status/column-statuses.component';
import { TicketsComponent } from './tickets/tickets.component';
import { TaksComponent } from './Tasks/tasks.component';
import { ProgressTicketComponent } from './tickets/progress-ticket/progress-ticket.component'
import { ProgressTaskComponent } from './Tasks/progress-task/progress-task.component';
// import { DragDropComponent } from './tickets/progress-ticket-2/progress-ticket-2.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'projects', component: ProjectsComponent, canActivate: [AppRouteGuard] },
                    { path: 'progress-ticket', component: ProgressTicketComponent, canActivate: [AppRouteGuard] },
                    { path: 'tickets', component: TicketsComponent, canActivate: [AppRouteGuard] },
                    { path: 'progress-task', component: ProgressTaskComponent, canActivate: [AppRouteGuard] },
                    { path: 'tasks', component: TaksComponent, canActivate: [AppRouteGuard] },
                    { path: 'column-status', component: ColumnStatusesComponent, canActivate: [AppRouteGuard] },
                    // { path: 'progress-ticket-2', component: DragDropComponent, canActivate: [AppRouteGuard] },
                    { path: 'clients', component: ClientsComponent, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent, canActivate: [AppRouteGuard] },
                    { path: 'update-password', component: ChangePasswordComponent, canActivate: [AppRouteGuard] }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
