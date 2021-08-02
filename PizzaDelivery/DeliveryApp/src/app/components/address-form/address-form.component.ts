import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarComponent } from 'src/app/components/snackbar/snackbar.component';

@Component({
  selector: 'app-address-form',
  templateUrl: './address-form.component.html',
  styleUrls: ['./address-form.component.css']
})
export class AddressFormComponent {

  addressForm = this.fb.group({
    firstName: [null, Validators.required],
    lastName: [null, Validators.required],
    phone: [null, Validators.required],
    address: [{ value: null, disabled: false }, Validators.required],
    city: [{ value: null, disabled: false }, Validators.required],
    state: [{ value: null, disabled: false }, Validators.required],
    zip: [{ value: null, disabled: false }, Validators.required],
    valid: [false, Validators.requiredTrue]
  });

  @Output() customerChanged: EventEmitter<FormGroup> = new EventEmitter();

  states = [
    { name: 'Alabama', abbreviation: 'AL' },
    { name: 'Alaska', abbreviation: 'AK' },
    { name: 'Arizona', abbreviation: 'AZ' },
    { name: 'Arkansas', abbreviation: 'AR' },
    { name: 'California', abbreviation: 'CA' },
    { name: 'Colorado', abbreviation: 'CO' },
    { name: 'Connecticut', abbreviation: 'CT' },
    { name: 'Delaware', abbreviation: 'DE' },
    { name: 'District Of Columbia', abbreviation: 'DC' },
    { name: 'Florida', abbreviation: 'FL' },
    { name: 'Georgia', abbreviation: 'GA' },
    { name: 'Guam', abbreviation: 'GU' },
    { name: 'Hawaii', abbreviation: 'HI' },
    { name: 'Idaho', abbreviation: 'ID' },
    { name: 'Illinois', abbreviation: 'IL' },
    { name: 'Indiana', abbreviation: 'IN' },
    { name: 'Iowa', abbreviation: 'IA' },
    { name: 'Kansas', abbreviation: 'KS' },
    { name: 'Kentucky', abbreviation: 'KY' },
    { name: 'Louisiana', abbreviation: 'LA' },
    { name: 'Maine', abbreviation: 'ME' },
    { name: 'Maryland', abbreviation: 'MD' },
    { name: 'Massachusetts', abbreviation: 'MA' },
    { name: 'Michigan', abbreviation: 'MI' },
    { name: 'Minnesota', abbreviation: 'MN' },
    { name: 'Mississippi', abbreviation: 'MS' },
    { name: 'Missouri', abbreviation: 'MO' },
    { name: 'Montana', abbreviation: 'MT' },
    { name: 'Nebraska', abbreviation: 'NE' },
    { name: 'Nevada', abbreviation: 'NV' },
    { name: 'New Hampshire', abbreviation: 'NH' },
    { name: 'New Jersey', abbreviation: 'NJ' },
    { name: 'New Mexico', abbreviation: 'NM' },
    { name: 'New York', abbreviation: 'NY' },
    { name: 'North Carolina', abbreviation: 'NC' },
    { name: 'North Dakota', abbreviation: 'ND' },
    { name: 'Ohio', abbreviation: 'OH' },
    { name: 'Oklahoma', abbreviation: 'OK' },
    { name: 'Oregon', abbreviation: 'OR' },
    { name: 'Pennsylvania', abbreviation: 'PA' },
    { name: 'Rhode Island', abbreviation: 'RI' },
    { name: 'South Carolina', abbreviation: 'SC' },
    { name: 'South Dakota', abbreviation: 'SD' },
    { name: 'Tennessee', abbreviation: 'TN' },
    { name: 'Texas', abbreviation: 'TX' },
    { name: 'Utah', abbreviation: 'UT' },
    { name: 'Vermont', abbreviation: 'VT' },
    { name: 'Virginia', abbreviation: 'VA' },
    { name: 'Washington', abbreviation: 'WA' },
    { name: 'West Virginia', abbreviation: 'WV' },
    { name: 'Wisconsin', abbreviation: 'WI' },
    { name: 'Wyoming', abbreviation: 'WY' }
  ];

  constructor(private fb: FormBuilder, public snackBar: MatSnackBar) { }

  get address() {
    return this.addressForm.get('address').value;
  }

  get city() {
    return this.addressForm.get('city').value;
  }

  get state() {
    return this.addressForm.get('state').value;
  }

  get zip() {
    return this.addressForm.get('zip').value;
  }

  get firstName() {
    return this.addressForm.get('firstName').value;
  }

  get lastName() {
    return this.addressForm.get('lastName').value;
  }

  get phone() {
    return this.addressForm.get('phone').value;
  }

  get valid() {
    return this.addressForm.get('valid').value;
  }

  customerChange() {
    this.customerChanged.emit(this.addressForm);
  }

  get verifyButtonDisabled(): boolean {
    return !(this.firstName && this.lastName && this.phone && this.address && this.city && this.state && this.zip && !this.valid);
  }

  async verifyAddress(): Promise<any> {
    let message = "Address could not be verified.";
    let style = 'error-snackbar';
    const promise = await fetch(`https://us-street.api.smartystreets.com/street-address?street=${this.address}&city=${this.city}&state=${this.state}&zipcode=${this.zip}&match=strict&key=101457761082301642&auth-id=7e7776c9-0bc2-eb79-3434-b174b3dc622b&auth-token=jLpYZmAm448dp2K7rffN`)
      .then(res => res.json())
      .catch(err => Promise.reject(err));
    if (promise.length) {
      this.addressForm.patchValue({
        valid: true
      });
      this.addressForm.disable();
      message = "Address verified successfully!"
      style = 'success-snackbar';
    }

    this.snackBar.openFromComponent(SnackbarComponent, {
      data: message,
      panelClass: [style],
      duration: 5000
    })
  }
}
