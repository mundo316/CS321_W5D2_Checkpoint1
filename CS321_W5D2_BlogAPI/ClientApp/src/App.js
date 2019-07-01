import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Blog } from './components/Blog';
import { Post } from './components/Post';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route exact path='/blog/:blogId/post/:postId' component={Post} />
        <Route exact path='/blog/:blogId' component={Blog} />
      </Layout>
    );
  }
}
