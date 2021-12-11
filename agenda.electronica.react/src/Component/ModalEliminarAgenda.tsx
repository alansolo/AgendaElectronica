import { AgendaDto } from "../Model/Agenda";
import { Modal, ModalBody, ModalFooter, ModalHeader} from 'reactstrap'
import React, { useEffect, useState } from "react";
import axios from 'axios'

export default function ModalEliminarAgenda(props: modalEliminarAgendaProps)
{
    const baseUrlEliminar = process.env.React_App_baseUrlEliminar ? process.env.React_App_baseUrlEliminar : "";
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

    const eliminarAgendaDelete = async() =>
    {
        await axios.delete(baseUrlEliminar + "/" + agenda.id)
        .then(response =>
            {
                var index = props.listaAgenda.findIndex(item => item.id === agenda.id);

                if(index !== -1)
                {
                    delete props.listaAgenda[index];
                }

                props.abrirCerrarModalEliminarAgenda();
            })
        .catch(error =>
            {
                console.log(error);
            })
    };

    return(
        <>
            <Modal isOpen={props.abrirModalEliminarAgenda}>
                <ModalHeader>
                    Eliminar Usuario
                </ModalHeader>
                <ModalBody>
                    ¿Esta seguro que deseas eliminar el Usuario de la Agenda?
                </ModalBody>
                <ModalFooter>
                    <button className="btn btn-primary" onClick={() => eliminarAgendaDelete()}>Agregar</button>
                    <button className="btn btn-danger" onClick={(e) => props.abrirCerrarModalEliminarAgenda()}>Cancelar</button>
                </ModalFooter>
            </Modal>
        </>
    );
}

interface modalEliminarAgendaProps
{
    agenda: AgendaDto;
    listaAgenda: AgendaDto[];
    abrirModalEliminarAgenda: boolean;
    abrirCerrarModalEliminarAgenda(): void;
}