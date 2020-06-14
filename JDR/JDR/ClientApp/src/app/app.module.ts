import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AllstatComponent } from './GestionStat/allstat/allstat.component';
import { StatinfoComponent } from './GestionStat/statinfo/statinfo.component';
import { StatService } from './services/stat.service';
import { NewStatComponent } from './GestionStat/new-stat/new-stat.component';
import { NewStatUtilComponent } from './GestionStat/new-stat-util/new-stat-util.component';
import { EditstatComponent } from './GestionStat/editstat/editstat.component';
import { AllraceComponent } from './GestionRace/allrace/allrace.component';
import { RaceinfoComponent } from './GestionRace/raceinfo/raceinfo.component';
import { NewraceComponent } from './GestionRace/newrace/newrace.component';
import { EditraceComponent } from './GestionRace/editrace/editrace.component';
import { RaceService } from './services/race.service';
import { NewStatRaceComponent } from './GestionRace/new-stat-race/new-stat-race.component';
import { NewDeeRaceComponent } from './GestionRace/new-dee-race/new-dee-race.component';
import { NewClasseComponent } from './GestionClasse/new-classe/new-classe.component';
import { AllClasseComponent } from './GestionClasse/all-classe/all-classe.component';
import { ClasseinfoComponent } from './GestionClasse/classeinfo/classeinfo.component';
import { ClasseService } from './services/classe.service';
import { EditClasseComponent } from './GestionClasse/edit-classe/edit-classe.component';
import { AllsousraceComponent } from './GestionSousRace/allsousrace/allsousrace.component';
import { SousraceinfoComponent } from './GestionSousRace/sousraceinfo/sousraceinfo.component';
import { NewsousraceComponent } from './GestionSousRace/newsousrace/newsousrace.component';
import { EditsousraceComponent } from './GestionSousRace/editsousrace/editsousrace.component';
import { SousRaceService } from './services/sousrace.service';
import { InscriptionComponent } from './GestionUser/inscription/inscription.component';
import { ConectionComponent } from './GestionUser/conection/conection.component';
import { UserService } from './services/user.service';
import { AllPersoComponent } from './GestionPerso/all-perso/all-perso.component';
import { PersoInfoComponent } from './GestionPerso/perso-info/perso-info.component';
import { NewPersoComponent } from './GestionPerso/new-perso/new-perso.component';
import { PersoService } from './services/perso.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AllstatComponent,
    StatinfoComponent,
    NewStatComponent,
    NewStatUtilComponent,
    EditstatComponent,
    AllraceComponent,
    RaceinfoComponent,
    NewraceComponent,
    EditraceComponent,
    NewStatRaceComponent,
    NewDeeRaceComponent,
    NewClasseComponent,
    AllClasseComponent,
    ClasseinfoComponent,
    EditClasseComponent,
    AllsousraceComponent,
    SousraceinfoComponent,
    NewsousraceComponent,
    EditsousraceComponent,
    InscriptionComponent,
    ConectionComponent,
    AllPersoComponent,
    PersoInfoComponent,
    NewPersoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'allStat', component: AllstatComponent },
      { path: 'new-stat', component: NewStatComponent },
      { path: 'editstat/:id', component: EditstatComponent },
      { path: 'allRace', component: AllraceComponent },
      { path: 'newRace', component: NewraceComponent },
      { path: 'editRace/:id', component: EditraceComponent },
      { path: 'allClasse', component: AllClasseComponent },
      { path: 'newClasse', component: NewClasseComponent },
      { path: 'editClasse/:id', component: EditClasseComponent },
      { path: 'allSousRace', component: AllsousraceComponent },
      { path: 'newSousRace', component: NewsousraceComponent },
      { path: 'editSousRace/:id', component: EditsousraceComponent },
      { path: 'inscription', component: InscriptionComponent },
      { path: 'connection', component: ConectionComponent },
      { path: 'AllPerso', component: AllPersoComponent },
      { path: 'NewPerso', component: NewPersoComponent },
    ])
  ],
  providers: [StatService, RaceService, ClasseService, SousRaceService, UserService, PersoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
