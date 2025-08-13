<?php

use App\Models\TodoList;

class TodoListService extends Service {
    public function store(
        $name,
        $description,
        $userId
    ) {
        $todoList = new TodoList();
        $todoList->name = $name;
        $todoList->name = $description;
        $todoList->name = $userId;
        return $todoList->save();
    }
}