import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'moment'
})
export class MomentPipe implements PipeTransform {

  transform(value: string, args?: string): string {
    if (value != null) {
      let d = moment(value);
      if (d.isValid()) {
        let fmt = args || 'L H:mm';
        return d.format(fmt);
      }
    }
    return value;
  }
}
