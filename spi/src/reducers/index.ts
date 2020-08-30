import { combineReducers } from 'redux';
import { toDoCrudReducer } from './viewToDoReducers';
  
export const rootReducers = combineReducers({
  crudForm: toDoCrudReducer
});