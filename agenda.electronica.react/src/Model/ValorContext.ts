import React from "react";
import { AgendaDto } from "./Agenda";
const ValorContext = React.createContext<AgendaDto>({
    id: 0,
    name: '',
    middleName: '',
    lastName: '',
    gender: '',
    telephone:'',
    mobile: '',
    email: ''
});
export default ValorContext;