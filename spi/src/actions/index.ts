import {
    CREATE_TODO_REQUEST,
    CREATE_TODO_SUCCESS,
    UPDATE_TODO_REQUEST,
    DELETE_TODO_REQUEST,
    SWITCH_UPDATE_WINDOW,
    GET_ALL_TODOS_REQUEST,
    GET_ALL_TODOS_SUCCESS,
    TODO_REQUEST_FAILED,
    SWITCH_SIGNIN_WINDOW
} from './todo-action-types';

import ToDoModel from '../types'

export const requestFailed = () => ({
    type: TODO_REQUEST_FAILED
});

export const getAllTodosSuccess = (items: [] ) => ({
    type: GET_ALL_TODOS_SUCCESS,
    payload: { items }
});

export const getAllTodosRequest = (projectId: string ) => ({
    type: GET_ALL_TODOS_REQUEST,
    payload: { projectId }
});

export const createToDoRequest = (dto: ToDoModel  ) => ({
    type: CREATE_TODO_REQUEST,
    payload: {
        dto
    }
});

export const createToDoSuccess = (dtoId: string) => ({
    type: CREATE_TODO_SUCCESS,
    payload: {
        dtoId
    }
});

export const updateToDo = (id: number, label: string, text: string, estimate: number) => ({
    type: UPDATE_TODO_REQUEST,
    payload: {
        id,
        text,
        estimate,
        label
    }
});

export const deleteToDoRequest = (id: number) => ({
    type: DELETE_TODO_REQUEST,
    payload: {
        id
    }
});

export const deleteToDoSuccess = () => ({
    type: DELETE_TODO_REQUEST
});

export const switchUpdateWindow = (open: boolean ) => ({
    type: SWITCH_UPDATE_WINDOW,
    payload: {
        open
    }
});

export const switchSignInWindow = (isSignInOpen: boolean ) => ({
    type: SWITCH_SIGNIN_WINDOW,
    payload: {
        isSignInOpen
    }
});