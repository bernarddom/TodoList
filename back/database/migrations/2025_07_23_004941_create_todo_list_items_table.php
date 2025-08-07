<?php

use App\Models\TodoList;
use App\Models\User;
use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('todo_list_items', function (Blueprint $table) {
            $table->id();
            $table->string("name");
            $table->string("description");
            $table->foreignIdFor(User::class);
            $table->foreignIdFor(TodoList::class);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('todo_list_items');
    }
};
