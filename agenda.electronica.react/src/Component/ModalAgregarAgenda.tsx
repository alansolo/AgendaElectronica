import { AgendaDto } from "../Model/Agenda";
import { Modal, ModalBody, ModalFooter, ModalHeader} from 'reactstrap'
import React, { useEffect, useState } from "react";
import axios from 'axios'

export default function ModalAgregarAgenda(props: modalAgregarAgendaProps)
{
    const baseUrlAgregar = process.env.React_App_baseUrlAgregar ? process.env.React_App_baseUrlAgregar : "";
    const baseUrlEditar = process.env.React_App_baseUrlEditar ? process.env.React_App_baseUrlEditar : "";
    const token = process.env.React_App_token ? process.env.React_App_token : "";
    const[agenda, setAgenda] = useState<AgendaDto>({...props.agenda});

    useEffect(() =>
    {
        setAgenda(props.agenda);
    }, [props.agenda.name, props.agenda.middleName, props.agenda.lastName, props.agenda.lastName,
        props.agenda.gender, props.agenda.telephone, props.agenda.mobile, props.agenda.email]);

    axios.interceptors.request.use((config) =>
    {
        if (!config?.headers) {
            throw new Error(`El parametro 'config.headers' no esta definido.`);
        }
        config.headers.Authorization= `Bearer ${token}`;
        return config;
    },
    error =>
    {
        console.log(error);
    });

    const nuevoAgendaPost = async() =>
    {
        await axios.post(baseUrlAgregar,
            {
                agendaDto: agenda
            })
        .then(response =>
            {
                Object.assign(agenda, response.data.agenda);
                Object.assign(props.agenda, agenda);
                props.listaAgenda.push(agenda);

                props.abrirCerrarModalAgregarAgenda();
            })
        .catch(error =>
            {
                console.log(error);
            })
    };

    const editarAgendaPut = async() =>
    {
        await axios.put(baseUrlEditar + "/" + agenda.id,
            {
                agendaDto: agenda
            })
        .then(response =>
            {
                props.listaAgenda.map(agendaTemp =>
                    {
                        if(agendaTemp.id === agenda.id)
                        {
                            Object.assign(agendaTemp, agenda);
                        }
                    });

                Object.assign(props.agenda, agenda);

                props.abrirCerrarModalAgregarAgenda();
            })
        .catch(error =>
            {
                console.log(error);
            })
    };

    const changeInput = (e: React.ChangeEvent<HTMLInputElement>) =>
    {
        const value = e.target.value;
        setAgenda(
            {
            ...agenda,
            [e.target.name]: value
        }
        );
    }

    const updateProps = () =>
    {
        if(props.esGuardar)
        {
            nuevoAgendaPost();
        }
        else
        {
            editarAgendaPut();
        }
    }

    return(
        <>
            <Modal isOpen={props.abrirModalAgregarAgenda}>
                <ModalHeader>
                    {
                        props.esGuardar
                            ? "Agregar Usuario"
                            : "Editar Usuario"
                    }
                </ModalHeader>
                <ModalBody>
                    <div className="form-group">
                        <label>Nombre: </label>
                        <br/>
                        <input type="text" className="form-control" name="name" value={agenda?.name} onChange={(e) => changeInput(e)}/>
                        <br/>
                        <label>Apellido P.: </label>
                        <br/>
                        <input type="text" className="form-control" name="middleName" value={agenda?.middleName} onChange={(e) => changeInput(e)}/>
                        <br/>
                        <label>Apellido M.: </label>
                        <br/>
                        <input type="text" className="form-control" name="lastName" value={agenda?.lastName} onChange={(e) => changeInput(e)}/>
                        <br/>
                        <label>Genero: </label>
                        <br/>
                        <input type="text" className="form-control" name="gender" value={agenda?.gender} onChange={(e) => changeInput(e)}/>
                        <br/>
                        <label>Telefono: </label>
                        <br/>
                        <input type="text" className="form-control" name="telephone" value={agenda?.telephone} onChange={(e) => changeInput(e)}/>
                        <br/>
                        <label>Celular: </label>
                        <br/>
                        <input type="text" className="form-control" name="mobile" value={agenda?.mobile} onChange={(e) => changeInput(e)}/>
                        <br/>
                        <label>Email: </label>
                        <br/>
                        <input type="text" className="form-control" name="email" value={agenda?.email} onChange={(e) => changeInput(e)}/>
                        <br/>
                    </div>
                </ModalBody>
                <ModalFooter>
                    {
                        props.esGuardar
                        ? <button className="btn btn-primary" onClick={() => updateProps()}>Agregar</button>
                        : <button className="btn btn-primary" onClick={() => updateProps()}>Editar</button>
                    }
                    <button className="btn btn-danger" onClick={(e) => props.abrirCerrarModalAgregarAgenda()}>Cancelar</button>
                </ModalFooter>
            </Modal>
        </>
    );
}

interface modalAgregarAgendaProps
{
    agenda: AgendaDto;
    listaAgenda: AgendaDto[];
    esGuardar: boolean;
    abrirModalAgregarAgenda: boolean;
    abrirCerrarModalAgregarAgenda(): void;
}