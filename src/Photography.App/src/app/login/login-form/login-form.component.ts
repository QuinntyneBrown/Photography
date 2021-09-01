import { EventEmitter, Output } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {

  @Output() public login: EventEmitter<any> = new EventEmitter();

  public form: FormGroup = new FormGroup({
    username: new FormControl(null,[Validators.required]),
    password: new FormControl(null,[Validators.required])
  });
}
