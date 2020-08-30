import { all, takeLatest, call, put, select } from 'redux-saga/effects';

import todoServiceClient from '../clientApi';

import {
    CREATE_TODO_REQUEST,
    UPDATE_TODO_REQUEST,
    DELETE_TODO_REQUEST,
    GET_ALL_TODOS_REQUEST
} from '../actions/todo-action-types';

import {
    getAllTodosRequest,
    createToDoSuccess,
    getAllTodosSuccess,
    requestFailed
} from '../actions';

function* watchGetAllTodosAsync() {
    try {
        const state = yield select();
        const projectId = '1'; //some projectId
        //yield put(getAllTodosRequest(projectId));
        const data = yield call(() => todoServiceClient().getAllTodos(projectId)  );
        yield put(getAllTodosSuccess(data));
    } catch (error) {
        yield put(requestFailed());
    }
}

function* watchCreateTodoAsync(action) {
    try {
        const state = yield select();
        const dto = action.payload.dto; 
        console.log(dto);
        
        //yield put(getAllTodosRequest(projectId));
        yield call(() => todoServiceClient().createTodo(dto) );
        yield put(getAllTodosRequest("1"));
    } catch (error) {
        console.log(error);
        
        yield put(requestFailed());
    }
}

function* watchDeleteTodoAsync(action) {
    try {
        const state = yield select();
        console.log('state');
        console.log(state);
        
        const id = action.payload.id; 
        console.log(id);
        yield call(() => todoServiceClient().deleteTodo(id) );
        yield put(getAllTodosRequest("1"));
    } catch (error) {
        console.log(error);
        
        yield put(requestFailed());
    }
}

export default function* watchCrudUpdate() {
    yield all([
        takeLatest(GET_ALL_TODOS_REQUEST, watchGetAllTodosAsync),
        takeLatest(CREATE_TODO_REQUEST, watchCreateTodoAsync),
        takeLatest(DELETE_TODO_REQUEST, watchDeleteTodoAsync)
    ]);
}