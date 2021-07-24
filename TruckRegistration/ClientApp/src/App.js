import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchTrucks } from './components/FetchTrucks';
import { AddTruck } from './components/AddTruck';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/trucks' component={FetchTrucks} />
        <Route path='/add-truck' component={AddTruck} />
      </Layout>
    );
  }
}
