import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit {

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {
    this.route.params.subscribe(params=>{
       this.studentID=<number>params['id'];
    })
  }

  novi_upis:any;
  studentID:any;
  student:any;
  akademske:any;
  upisi:any;
  ovjera_upis:any;

  ovjeriLjetni(s:any) {

  }

  upisLjetni(s:any) {

  }

  ovjeriZimski(s:any) {

  }

  ngOnInit(): void {
    this.GetStudent();
    this.fetchAkademske();
    this.fetchUpise();
  }

  private GetStudent() {

    this.httpKlijent.get(MojConfig.adresa_servera+ "/GetStudent",{
      headers:{
        "autentifikacija-token" : AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.vrijednost
      },

      params:{studentid:this.studentID},
      observe:'response'


    }).subscribe(response=>{

      if(response.status == 200){
        this.student = response.body;
      }


    })


  }

  NoviUpis() {
    this.novi_upis={
      evidentiraoid: AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalogId,
      studentid:this.studentID,
    }
  }

  private fetchAkademske() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/GetAllAkademske", MojConfig.http_opcije()).subscribe(x=>{
      this.akademske = x;
    });
  }

  private fetchUpise() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/GetUpisiStudenta",{
      headers:{
        "autentifikacija-token" : AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.vrijednost
      },

      params:{studentid:this.studentID},
      observe:'response'


    }).subscribe(response=>{

      if(response.status == 200){
        this.upisi = response.body;
      }


    })
  }

  DodajUpis() {
    this.httpKlijent.post(MojConfig.adresa_servera + '/DodajUpis',this.novi_upis, MojConfig.http_opcije()).subscribe(x=>{

      this.novi_upis = null;
      this.ngOnInit();
      porukaSuccess("Upis dodan");
    },error => {
      porukaError("Upis iste godine vazi samo ako je obnova");
    })
  }

  DodajOvjeru() {
    this.httpKlijent.post(MojConfig.adresa_servera + '/OvjeraUpis',this.ovjera_upis, MojConfig.http_opcije()).subscribe(x=>{

      this.ovjera_upis = null;
      this.ngOnInit();
      porukaSuccess("Upis ovjeren");
    },error => {
      porukaError("Upis nije ovjeren");
    })
  }
}
